using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers; // Token Header'ı için mutlaka ekle
using UdemyCarBook.Dto.BlogDtos;

namespace UdemyCarBook.WebUl.ViewComponents.BlogViewComponents
{
    public class _BlogDetailsRecentBlogsComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _BlogDetailsRecentBlogsComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // 1. Token'ı Claim'lerden çekiyoruz
            var token = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            var client = _httpClientFactory.CreateClient();

            if (!string.IsNullOrEmpty(token))
            {
                // 2. API'ye token'ı ekleyerek gidiyoruz
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseMessange = await client.GetAsync("https://localhost:7003/api/Blogs/GetLast3BlogsWithAuthorsList");
                if (responseMessange.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessange.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultLast3BlogsWithAuthors>>(jsonData);
                    return View(values);
                }
            }

            // 3. KRİTİK: Hata anında boş liste dönüyoruz ki sayfa patlamasın
            return View(new List<ResultLast3BlogsWithAuthors>());
        }
    }
}