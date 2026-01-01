using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers; // Bunu eklemeyi unutma!
using UdemyCarBook.Dto.ServiceDtos;

namespace UdemyCarBook.WebUl.ViewComponents.ServiceComponents
{
    public class _ServiceComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ServiceComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // 1. Kullanıcının giriş anahtarını (Token) alıyoruz
            var token = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            var client = _httpClientFactory.CreateClient();

            if (token != null)
            {
                // 2. API'ye "Ben yetkiliyim, işte anahtarım" diyoruz
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseMessage = await client.GetAsync("https://localhost:7003/api/Services");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultServiceDto>>(jsonData);
                    return View(values);
                }
            }

            // 3. KRİTİK: return View(); yerine BOŞ LİSTE dönüyoruz
            // Bu sayede Index.cshtml patlamaz, sadece hizmetler kısmı boş görünür.
            return View(new List<ResultServiceDto>());
        }
    }
}