using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers; // Token Header'ı için mutlaka ekle
using UdemyCarBook.Dto.TagCloudDtos;

namespace UdemyCarBook.WebUl.ViewComponents.BlogViewComponents
{
    public class _BlogDetailCloudTagByBlogComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _BlogDetailCloudTagByBlogComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.blogid = id;

            // 1. Token'ı Claim'lerden çekiyoruz
            var token = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            var client = _httpClientFactory.CreateClient();

            if (!string.IsNullOrEmpty(token))
            {
                // 2. API'ye yetkili olduğumuzu bildiriyoruz
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseMessange = await client.GetAsync($"https://localhost:7003/api/TagClouds/GetTagCloudByBlogId?id=" + id);
                if (responseMessange.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessange.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<GetByBlogIdTagCloudDto>>(jsonData);
                    return View(values);
                }
            }

            // 3. KRİTİK: Hata anında veya token yoksa BOŞ LİSTE dönüyoruz
            // Bu satır sayesinde "requires a model item of type List" hatası tamamen bitecek!
            return View(new List<GetByBlogIdTagCloudDto>());
        }
    }
}