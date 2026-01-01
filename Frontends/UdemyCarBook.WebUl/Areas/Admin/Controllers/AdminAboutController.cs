//using System.Net.Http.Headers;
//using System.Text;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using UdemyCarBook.Dto.AboutDtos;

//namespace UdemyCarBook.WebUl.Areas.Admin.Controllers
//{
//    [Authorize(Roles = "Admin,Manger")]
//    [Area("Admin")]
//    [Route("Admin/AdminAbout")]
//    public class AdminAboutController : Controller
//    {
//        private readonly IHttpClientFactory _httpClientFactory;

//        public AdminAboutController(IHttpClientFactory httpClientFactory)
//        {
//            _httpClientFactory = httpClientFactory;
//        }

//        [Route("Index")]

//        public async Task<IActionResult> Index()
//        {
//            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
//            if (token != null)
//            {
//                var client = _httpClientFactory.CreateClient();
//                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
//                var responseMessange = await client.GetAsync("https://localhost:7003/api/Abouts");
//                if (responseMessange.IsSuccessStatusCode)
//                {
//                    var jsonData = await responseMessange.Content.ReadAsStringAsync();
//                    var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);
//                    return View(values);
//                }
//            }

//            return View();
//        }

//        [HttpGet]
//        [Route("CreateAbout")]
//        public IActionResult CreateAbout()
//        {
//            return View();
//        }

//        [HttpPost]
//        [Route("CreateAbout")]

//        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
//        {
//            var client = _httpClientFactory.CreateClient();
//            var jsonData = JsonConvert.SerializeObject(createAboutDto);
//            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
//            var responsemessage = await client.PostAsync("https://localhost:7003/api/Abouts", stringContent);
//            if (responsemessage.IsSuccessStatusCode)
//            {
//                return RedirectToAction("Index", "AdminAbout", new { area = "Admin" });
//            }
//            return View();
//        }

//        [Route("RemoveAbout/{id}")]

//        public async Task<IActionResult> RemoveAbout(int id)
//        {
//            var client = _httpClientFactory.CreateClient();
//            var responseMessage = await client.DeleteAsync("https://localhost:7003/api/Abouts?id=" + id);
//            if (responseMessage.IsSuccessStatusCode)
//            {
//                return RedirectToAction("Index", "AdminAbout", new { area = "Admin" });
//            }
//            return View();
//        }

//        [HttpGet]

//        [Route("UpdateAbout/{id}")]

//        public async Task<IActionResult> UpdateAbout(int id)
//        {
//            var client = _httpClientFactory.CreateClient();
//            var responseMessage = await client.GetAsync($"https://localhost:7003/api/Abouts/{id}");
//            if (responseMessage.IsSuccessStatusCode)
//            {
//                var jsonData = await responseMessage.Content.ReadAsStringAsync();
//                var values = JsonConvert.DeserializeObject<UpdateAboutDto>(jsonData);
//                return View(values);
//            }
//            return View();
//        }

//        [HttpPost]
//        [Route("UpdateAbout/{id}")]

//        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
//        {
//            var client = _httpClientFactory.CreateClient();
//            var jsonData = JsonConvert.SerializeObject(updateAboutDto);
//            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
//            var responseMessage = await client.PutAsync("https://localhost:7003/api/Abouts/", stringContent);
//            if (responseMessage.IsSuccessStatusCode)
//            {
//                return RedirectToAction("Index", "AdminAbout", new { area = "Admin" });
//            }
//            return View();
//        }
//    }
//}


using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UdemyCarBook.Dto.AboutDtos;

namespace UdemyCarBook.WebUl.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Manger")] // Manager yetkisi eklendi
    [Area("Admin")]
    [Route("Admin/AdminAbout")]
    public class AdminAboutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminAboutController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // HttpClient'a token ekleyen yardımcı metot
        private void SetToken(HttpClient client)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            SetToken(client); // Token ekle
            var responseMessage = await client.GetAsync("https://localhost:7003/api/Abouts");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);
                return View(values);
            }
            return View(new List<ResultAboutDto>()); // Boş liste dön, sayfa patlamasın
        }

        [HttpGet]
        [Route("CreateAbout")]
        public IActionResult CreateAbout() => View();

        [HttpPost]
        [Route("CreateAbout")]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            var client = _httpClientFactory.CreateClient();
            SetToken(client);
            var jsonData = JsonConvert.SerializeObject(createAboutDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7003/api/Abouts", content);
            if (response.IsSuccessStatusCode) return RedirectToAction("Index");
            return View();
        }

        [Route("RemoveAbout/{id}")]
        public async Task<IActionResult> RemoveAbout(int id)
        {
            var client = _httpClientFactory.CreateClient();
            SetToken(client);
            var response = await client.DeleteAsync($"https://localhost:7003/api/Abouts?id={id}");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("UpdateAbout/{id}")]
        public async Task<IActionResult> UpdateAbout(int id)
        {
            var client = _httpClientFactory.CreateClient();
            SetToken(client);
            var response = await client.GetAsync($"https://localhost:7003/api/Abouts/{id}");
            if (response.IsSuccessStatusCode)
            {
                var values = JsonConvert.DeserializeObject<UpdateAboutDto>(await response.Content.ReadAsStringAsync());
                return View(values);
            }
            return View();
        }

        [HttpPost]
        [Route("UpdateAbout/{id}")]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            var client = _httpClientFactory.CreateClient();
            SetToken(client);
            var content = new StringContent(JsonConvert.SerializeObject(updateAboutDto), Encoding.UTF8, "application/json");
            var response = await client.PutAsync("https://localhost:7003/api/Abouts/", content);
            if (response.IsSuccessStatusCode) return RedirectToAction("Index");
            return View();
        }
    }
}