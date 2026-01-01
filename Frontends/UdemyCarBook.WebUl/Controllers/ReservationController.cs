using System.Net.Http;
using System.Net.Http.Headers; // Token için şart
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using UdemyCarBook.Dto.LocationDtos;
using UdemyCarBook.Dto.ReservationDtos;

namespace UdemyCarBook.WebUl.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReservationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReservationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.v1 = "Araç Kiralama";
            ViewBag.v2 = "Araç Rezervasyon Formu";
            ViewBag.v3 = id;

            // 1. Token'ı Claim'lerden alıyoruz
            var token = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            var client = _httpClientFactory.CreateClient();

            if (!string.IsNullOrEmpty(token))
            {
                // 2. API'ye anahtarı veriyoruz
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseMessange = await client.GetAsync("https://localhost:7003/api/Locations");

                if (responseMessange.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessange.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultLocationDto>>(jsonData);

                    // 3. Null kontrolü yaparak SelectList'i oluşturuyoruz
                    if (values != null)
                    {
                        List<SelectListItem> values2 = (from x in values
                                                        select new SelectListItem
                                                        {
                                                            Text = x.Name,
                                                            Value = x.LocationID.ToString()
                                                        }).ToList();
                        ViewBag.v = values2;
                        return View();
                    }
                }
            }

            // Eğer bir şeyler ters giderse boş liste gönder ki sayfa patlamasın
            ViewBag.v = new List<SelectListItem>();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateReservationDto createReservationDto)
        {
            var token = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            var client = _httpClientFactory.CreateClient();

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var jsonData = JsonConvert.SerializeObject(createReservationDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var responsemessage = await client.PostAsync("https://localhost:7003/api/Reservations", stringContent);

                if (responsemessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Default");
                }
            }

            // --- KRİTİK NOKTA BURASI KANKA ---
            // Eğer buraya düştüyse kayıt başarısız olmuştur. 
            // Sayfayı tekrar yüklemeden önce Dropdown listesini (Lokasyonları) yeniden çekip doldurmalısın!

            var responseLoc = await client.GetAsync("https://localhost:7003/api/Locations");
            if (responseLoc.IsSuccessStatusCode)
            {
                var jsonDataLoc = await responseLoc.Content.ReadAsStringAsync();
                var valuesLoc = JsonConvert.DeserializeObject<List<ResultLocationDto>>(jsonDataLoc);

                List<SelectListItem> values2 = (from x in valuesLoc
                                                select new SelectListItem
                                                {
                                                    Text = x.Name,
                                                    Value = x.LocationID.ToString()
                                                }).ToList();
                ViewBag.v = values2; // Listeyi canlandırdık
            }
            else
            {
                ViewBag.v = new List<SelectListItem>(); // En azından boş liste gönder ki patlamasın
            }

            return View(createReservationDto); // Modelini de geri gönder ki yazdıkların silinmesin
        }





        //[HttpPost]
        //public async Task<IActionResult> Index(CreateReservationDto createReservationDto)
        //{
        //    var token = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
        //    var client = _httpClientFactory.CreateClient();

        //    if (!string.IsNullOrEmpty(token))
        //    {
        //        // Kayıt atarken de yetki (token) göndermek zorundayız
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //        var jsonData = JsonConvert.SerializeObject(createReservationDto);
        //        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        //        var responsemessage = await client.PostAsync("https://localhost:7003/api/Reservations", stringContent);

        //        if (responsemessage.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("Index", "Default");
        //        }
        //    }

        //    return View();
    }
    
}