//using System.Net.Http;
//using System.Text;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using UdemyCarBook.Dto.BrandDtos;
//using UdemyCarBook.Dto.RentACarDtos;

//namespace UdemyCarBook.WebUl.Controllers
//{
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//    public class RentACarListController : Controller
//    {
//        private readonly IHttpClientFactory _httpClientFactory;

//        public RentACarListController(IHttpClientFactory httpClientFactory)
//        {
//            _httpClientFactory = httpClientFactory;
//        }
//        public async Task<IActionResult>  Index(int id)
//        {
//            var locationID = TempData["locationID"];

//            id = int.Parse(locationID.ToString());

//            ViewBag.locationID = locationID;

//            var client = _httpClientFactory.CreateClient();
//            var responseMessange = await client.GetAsync($"https://localhost:7003/api/RentACars?locationID={id}&available=true");
//            if (responseMessange.IsSuccessStatusCode)
//            {
//                var jsonData = await responseMessange.Content.ReadAsStringAsync();
//                var values = JsonConvert.DeserializeObject<List<FilterRentACarDto>>(jsonData);
//                return View(values);
//            }
//            return View();

//        }
//    }
//}

using System.Net.Http.Headers; // Token için mutlaka ekle
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UdemyCarBook.Dto.RentACarDtos;

namespace UdemyCarBook.WebUl.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RentACarListController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RentACarListController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int id)
        {
            // 1. Lokasyon ID'sini alıyoruz
            var locationID = TempData["locationID"];
            if (locationID == null)
            {
                return RedirectToAction("Index", "Default");
            }

            id = int.Parse(locationID.ToString());
            ViewBag.locationID = locationID;

            // 2. Token'ı Claim'den çekiyoruz
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            var client = _httpClientFactory.CreateClient();

            if (token != null)
            {
                // 3. API'ye anahtarımızı (Bearer Token) sunuyoruz
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseMessange = await client.GetAsync($"https://localhost:7003/api/RentACars?locationID={id}&available=true");

                if (responseMessange.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessange.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<FilterRentACarDto>>(jsonData);
                    return View(values);
                }
            }

            // 4. KRİTİK: Hata anında (veya token yoksa) null değil, BOŞ LİSTE dönüyoruz
            // Bu sayede image_95a573.jpg'deki o kırmızı hatayı artık görmeyeceksin.
            return View(new List<FilterRentACarDto>());
        }
    }
}