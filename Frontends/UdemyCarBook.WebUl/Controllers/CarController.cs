//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using UdemyCarBook.Dto.CarDtos;
//using UdemyCarBook.Dto.CarPricingDtos;

//namespace UdemyCarBook.WebUl.Controllers
//{
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//    public class CarController : Controller
//    {
//        private readonly IHttpClientFactory _httpClientFactory;

//        public CarController(IHttpClientFactory httpClientFactory)
//        {
//            _httpClientFactory = httpClientFactory;
//        }

//        public async Task<IActionResult> Index()
//        {
//            ViewBag.v1 = "Araçlarımız";
//            ViewBag.v2 = "Aracınızı seçiniz";
//            var client = _httpClientFactory.CreateClient();
//            var responseMessange = await client.GetAsync("https://localhost:7003/api/CarPricings");
//            if (responseMessange.IsSuccessStatusCode)
//            {
//                var jsonData = await responseMessange.Content.ReadAsStringAsync();
//                var values = JsonConvert.DeserializeObject<List<ResultCarPricingWithCarDto>>(jsonData);
//                return View(values);
//            }
//            return View();
//        }

//        public async Task<IActionResult> CarDetail(int id)
//        {
//            ViewBag.v1 = "Araç Detayları";
//            ViewBag.v2 = "Aracın Teknik Aksesuar ve Özellikleri";
//            ViewBag.carId = id;
//            return View();
//        }
//    }
//}


using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using UdemyCarBook.Dto.CarPricingDtos;

namespace UdemyCarBook.WebUI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CarController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CarController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Araçlarımız";
            ViewBag.v2 = "Aracınızı seçiniz";

            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.GetAsync("https://localhost:7003/api/CarPricings");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultCarPricingWithCarDto>>(jsonData);
                    return View(values);
                }
            }
            return View(new List<ResultCarPricingWithCarDto>());
        }

        // Kanka 'id' parametresi buradan giriyor, ViewBag.carId ile sayfaya taşınıyor
        public IActionResult CarDetail(int id)
        {
            ViewBag.v1 = "Araç Detayları";
            ViewBag.v2 = "Aracın Teknik Aksesuar ve Özellikleri";
            ViewBag.carId = id; // Buradaki isme dikkat!
            return View();
        }
    }
}