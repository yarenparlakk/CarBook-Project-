//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using UdemyCarBook.Dto.FooterAddressDtos;

//namespace UdemyCarBook.WebUl.ViewComponents.FooterAddressComponents
//{
//    public class _FooterAddressComponantPartial : ViewComponent
//    {
//        private readonly IHttpClientFactory _httpClientFactory;

//        public _FooterAddressComponantPartial(IHttpClientFactory httpClientFactory)
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
using System.Net.Http.Headers; // Token için mutlaka ekle kanka
using UdemyCarBook.Dto.FooterAddressDtos;

namespace UdemyCarBook.WebUl.ViewComponents.FooterAddressComponents
{
    public class _FooterAddressComponantPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _FooterAddressComponantPartial(IHttpClientFactory httpClientFactory)
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

                var responseMessange = await client.GetAsync("https://localhost:7003/api/FooterAddresses");
                if (responseMessange.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessange.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultFooterAddressDto>>(jsonData);
                    return View(values);
                }
            }

            // 3. KRİTİK DÜZELTME: Hata anında null değil, boş liste dönüyoruz
            // Bu sayede image_958bc4.jpg'deki o kırmızı hatayı artık almayacaksın.
            return View(new List<ResultFooterAddressDto>());
        }
    }
}
