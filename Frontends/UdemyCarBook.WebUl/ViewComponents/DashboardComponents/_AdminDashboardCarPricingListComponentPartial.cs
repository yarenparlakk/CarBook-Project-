using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using UdemyCarBook.Dto.CarPricingDtos;

namespace UdemyCarBook.WebUl.ViewComponents.DashboardComponents
{
    public class _AdminDashboardCarPricingListComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _AdminDashboardCarPricingListComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // 1. Giriş yapan kullanıcının token'ını (anahtarını) alıyoruz
            var token = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            var client = _httpClientFactory.CreateClient();

            if (token != null)
            {
                // 2. API'ye "Ben yetkiliyim, işte anahtarım" diyoruz
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseMessage = await client.GetAsync("https://localhost:7003/api/CarPricings/GetCarPricingWithTimePeriodList");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultCarPricingListWithModelDto>>(jsonData);
                    return View(values);
                }
            }

            // 3. KRİTİK: Eğer token yoksa veya API hata verdiyse View patlamasın diye BOŞ LİSTE gönderiyoruz
            // Bu sayede Model asla null gelmez, döngü hata vermez.
            return View(new List<ResultCarPricingListWithModelDto>());
        }
    }
}