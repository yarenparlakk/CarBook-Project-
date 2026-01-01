using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers; // Token için mutlaka ekle
using UdemyCarBook.Dto.CategoryDtos;

namespace UdemyCarBook.WebUl.ViewComponents.BlogViewComponents
{
    public class _BlogDetailsCategoryComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _BlogDetailsCategoryComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // 1. Kullanıcının giriş anahtarını (Token) claim'lerden çekiyoruz
            var token = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            var client = _httpClientFactory.CreateClient();

            if (!string.IsNullOrEmpty(token))
            {
                // 2. API'ye "Ben yetkiliyim, veriyi gönder" diyoruz
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseMessange = await client.GetAsync("https://localhost:7003/api/Categories");
                if (responseMessange.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessange.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                    return View(values);
                }
            }

            // 3. KRİTİK: Hata anında null değil, BOŞ LİSTE dönüyoruz
            // Bu sayede image_d11c67.jpg'deki o kırmızı hatayı artık almayacaksın.
            return View(new List<ResultCategoryDto>());
        }
    }
}