//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using UdemyCarBook.Dto.FooterAddressDtos;
//using UdemyCarBook.Dto.TestimonialDtos;

//namespace UdemyCarBook.WebUl.ViewComponents.UILayoutViewComponents
//{
//    public class _FooterUILayoutComponentPartial : ViewComponent
//    {
//        private readonly IHttpClientFactory _httpClientFactory;

//        public _FooterUILayoutComponentPartial(IHttpClientFactory httpClientFactory)
//        {
//            _httpClientFactory = httpClientFactory;
//        }

//        public async Task<IViewComponentResult> InvokeAsync()
//        {
//            var client = _httpClientFactory.CreateClient();
//            var responseMessange = await client.GetAsync("https://localhost:7003/api/FooterAddresses");
//            if (responseMessange.IsSuccessStatusCode)
//            {
//                var jsonData = await responseMessange.Content.ReadAsStringAsync();
//                var values = JsonConvert.DeserializeObject<List<ResultFooterAddressDto>>(jsonData);
//                return View(values);
//            }
//            return View();
//        }
//    }
//}


using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using UdemyCarBook.Dto.FooterAddressDtos;

namespace UdemyCarBook.WebUl.ViewComponents.UILayoutViewComponents
{
    public class _FooterUILayoutComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _FooterUILayoutComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // 1. Token'ı alıyoruz (Layout içinden erişmek için HttpContext kullanılır)
            var token = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;

            var client = _httpClientFactory.CreateClient();

            if (token != null)
            {
                // 2. API'ye anahtarı veriyoruz ki 401 hatası almayalım
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var responseMessange = await client.GetAsync("https://localhost:7003/api/FooterAddresses");

            if (responseMessange.IsSuccessStatusCode)
            {
                var jsonData = await responseMessange.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFooterAddressDto>>(jsonData);
                return View(values);
            }

            // 3. KRİTİK NOKTA: Hata olsa bile mutlaka BOŞ LİSTE dönüyoruz
            // Böylece "Model uyuşmazlığı" hatası almazsın.
            return View(new List<ResultFooterAddressDto>());
        }
    }
}