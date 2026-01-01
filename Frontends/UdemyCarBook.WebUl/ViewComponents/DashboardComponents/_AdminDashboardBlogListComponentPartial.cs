//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using UdemyCarBook.Dto.BlogDtos;

//namespace UdemyCarBook.WebUl.ViewComponents.DashboardComponents
//{
//    public class _AdminDashboardBlogListComponentPartial : ViewComponent
//    {
//        private readonly IHttpClientFactory _httpClientFactory;

//        public _AdminDashboardBlogListComponentPartial(IHttpClientFactory httpClientFactory)
//        {
//            _httpClientFactory = httpClientFactory;
//        }


//        public async Task<IViewComponentResult> InvokeAsync()
//        {
//            var client = _httpClientFactory.CreateClient();
//            var responseMessange = await client.GetAsync("https://localhost:7003/api/Blogs/GetAllBlogsWithAuthorList");
//            if (responseMessange.IsSuccessStatusCode)
//            {
//                var jsonData = await responseMessange.Content.ReadAsStringAsync();
//                var values = JsonConvert.DeserializeObject<List<ResultBlogDto>>(jsonData);
//                return View(values);
//            }
//            return View(new List<ResultBlogDto>());
//        }
//    }
//}


using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers; // Token için ekledik
using UdemyCarBook.Dto.BlogDtos;

namespace UdemyCarBook.WebUl.ViewComponents.DashboardComponents
{
    public class _AdminDashboardBlogListComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _AdminDashboardBlogListComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // 1. Token'ı Claim'den çekiyoruz
            var token = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            var client = _httpClientFactory.CreateClient();

            if (!string.IsNullOrEmpty(token))
            {
                // 2. HttpClient'a anahtarı (Bearer Token) ekliyoruz
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseMessange = await client.GetAsync("https://localhost:7003/api/Blogs/GetAllBlogsWithAuthorList");
                if (responseMessange.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessange.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultBlogDto>>(jsonData);
                    return View(values);
                }
            }

            // 3. Hata durumunda boş liste dönüyoruz ki sayfa patlamasın
            return View(new List<ResultBlogDto>());
        }
    }
}