using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers; // Bunu eklemeyi unutma!
using UdemyCarBook.Dto.BlogDtos;

namespace UdemyCarBook.WebUl.ViewComponents.BlogViewComponents
{
    public class _GetLast3BlogsWithAuthorListComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _GetLast3BlogsWithAuthorListComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // 1. Kullanıcının anahtarını alıyoruz
            var token = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            var client = _httpClientFactory.CreateClient();

            if (token != null)
            {
                // 2. API'ye anahtarı ekliyoruz
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseMessange = await client.GetAsync("https://localhost:7003/api/Blogs/GetLast3BlogsWithAuthorsList");
                if (responseMessange.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessange.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultLast3BlogsWithAuthors>>(jsonData);
                    return View(values);
                }
            }

            // 3. KRİTİK: Hata anında boş liste dönüyoruz
            return View(new List<ResultLast3BlogsWithAuthors>());
        }
    }
}