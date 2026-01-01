using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers; // Token Header'ı için mutlaka ekle
using UdemyCarBook.Dto.CarDtos;

namespace UdemyCarBook.WebUl.ViewComponents.CarDetailViewComponents
{
    public class _CarDetailMainCarFeatureComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _CarDetailMainCarFeatureComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.carId = id;

            // 1. Token'ı Claim'lerden çekiyoruz
            var token = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            var client = _httpClientFactory.CreateClient();

            if (!string.IsNullOrEmpty(token))
            {
                // 2. API'ye yetkili olduğumuzu bildiriyoruz
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseMessage = await client.GetAsync($"https://localhost:7003/api/Cars/{id}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<ResultCarWithBrandsDto>(jsonData);
                    return View(values);
                }
            }

            // 3. KRİTİK: Hata anında veya token yoksa null değil, BOŞ NESNE dönüyoruz
            // Bu sayede View tarafındaki @Model.BrandName gibi çağrılar sayfayı patlatmaz.
            return View(new ResultCarWithBrandsDto());
        }
    }
}