using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers; // Bunu eklemeyi unutma kanka
using UdemyCarBook.Dto.BannerDtos;

namespace UdemyCarBook.WebUl.ViewComponents.DefaultViewComponents
{
    public class _DefaultCoverUILayoutComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DefaultCoverUILayoutComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // 1. Kullanıcının giriş anahtarını (Token) claim'lerden alıyoruz
            var token = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            var client = _httpClientFactory.CreateClient();

            if (!string.IsNullOrEmpty(token))
            {
                // 2. API'ye "Ben yetkili bir kullanıcıyım" diyoruz
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var responseMessange = await client.GetAsync("https://localhost:7003/api/Banners");
            if (responseMessange.IsSuccessStatusCode)
            {
                var jsonData = await responseMessange.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBannerDto>>(jsonData);
                return View(values);
            }

            // 3. Eğer hata olursa model uyuşmazlığı (null hatası) olmasın diye boş liste dönüyoruz
            return View(new List<ResultBannerDto>());
        }
    }
}