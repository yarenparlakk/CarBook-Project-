using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using UdemyCarBook.Dto.LocationDtos;
using UdemyCarBook.WebUl.Models;

namespace UdemyCarBook.WebUl.Controllers
{
    [AllowAnonymous] // Kanka burası önemli, ana sayfa girişe zorlamasın ki döngü kırılsın
    public class DefaultController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            var values = new List<ResultLocationDto>();

            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseMessage = await client.GetAsync("https://localhost:7003/api/Locations");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    values = JsonConvert.DeserializeObject<List<ResultLocationDto>>(jsonData);

                    // Dropdown (Seçim kutusu) için verileri hazırlıyoruz
                    List<SelectListItem> locationList = (from x in values
                                                         select new SelectListItem
                                                         {
                                                             Text = x.Name,
                                                             Value = x.LocationID.ToString()
                                                         }).ToList();
                    ViewBag.v = locationList;
                }
            }

            // View'a modeli gönderiyoruz ki @foreach patlamasın
            return View(new ResultLocationDto());
        }

        [HttpPost]
        public IActionResult Index(string book_pick_date, string book_off_date, string time_pick, string time_off, string locationID)
        {
            TempData["bookpickdate"] = book_pick_date;
            TempData["bookoffdate"] = book_off_date;
            TempData["timepick"] = time_pick;
            TempData["timeoff"] = time_off;
            TempData["locationID"] = locationID;
            return RedirectToAction("Index", "RentACarList");
        }
    }
}