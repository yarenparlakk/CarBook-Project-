//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using UdemyCarBook.Dto.BlogDtos;

//namespace UdemyCarBook.WebUl.ViewComponents.BlogViewComponents
//{
//    public class _BlogDetailsMainComponentPartial : ViewComponent
//    {
//        private readonly IHttpClientFactory _httpClientFactory;

//        public _BlogDetailsMainComponentPartial(IHttpClientFactory httpClientFactory)
//        {
//            _httpClientFactory = httpClientFactory;
//        }

//        public async Task<IViewComponentResult> InvokeAsync(int id)
//        {
//            var client = _httpClientFactory.CreateClient();
//            var responseMessange = await client.GetAsync($"https://localhost:7003/api/Blogs/" + id);
//            if (responseMessange.IsSuccessStatusCode)
//            {
//                var jsonData = await responseMessange.Content.ReadAsStringAsync();
//                var values = JsonConvert.DeserializeObject<GetBlogById>(jsonData);
//                return View(values);
//            }

//            return View();
//        }
//    }
//}


using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using System.Net.Http.Headers; // Token Header'ı için mutlaka ekle

using UdemyCarBook.Dto.BlogDtos;



namespace UdemyCarBook.WebUl.ViewComponents.BlogViewComponents

{

    public class _BlogDetailsMainComponentPartial : ViewComponent

    {

        private readonly IHttpClientFactory _httpClientFactory;



        public _BlogDetailsMainComponentPartial(IHttpClientFactory httpClientFactory)

        {

            _httpClientFactory = httpClientFactory;

        }



        public async Task<IViewComponentResult> InvokeAsync(int id)

        {

            // 1. Token'ı Claim'lerden çekiyoruz

            var token = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;

            var client = _httpClientFactory.CreateClient();



            if (!string.IsNullOrEmpty(token))

            {

                // 2. API'ye "Ben yetkiliyim" diyerek token'ı ekliyoruz

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);



                var responseMessange = await client.GetAsync($"https://localhost:7003/api/Blogs/" + id);

                if (responseMessange.IsSuccessStatusCode)

                {

                    var jsonData = await responseMessange.Content.ReadAsStringAsync();

                    var values = JsonConvert.DeserializeObject<GetBlogById>(jsonData);

                    return View(values);

                }

            }



            // 3. KRİTİK: Hata anında veya token yoksa boş bir nesne dön ki sayfa patlamasın!

            // Not: Buradaki DTO adın View içinde neyse ona göre (GetBlogById gibi) kontrol et.

            return View(new GetBlogById());

        }

    }

}