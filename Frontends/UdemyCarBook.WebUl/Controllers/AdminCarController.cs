//using System.Net.Http.Headers;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Newtonsoft.Json;
//using UdemyCarBook.Dto.BrandDtos;
//using UdemyCarBook.Dto.CarDtos;

//namespace UdemyCarBook.WebUl.Controllers
//{
//    [Authorize(Roles = "Admin,Manager")] // ✅ Yazım düzeltildi
//    public class AdminCarController : Controller
//    {
//        private readonly IHttpClientFactory _httpClientFactory;

//        public AdminCarController(IHttpClientFactory httpClientFactory)
//        {
//            _httpClientFactory = httpClientFactory;
//        }

//        public async Task<IActionResult> Index()
//        {
//            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
//            if (token != null)
//            {
//                var client = _httpClientFactory.CreateClient();
//                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
//                var responseMessage = await client.GetAsync("https://localhost:7003/api/Cars/GetCarWithBrand");
//                if (responseMessage.IsSuccessStatusCode)
//                {
//                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
//                    var values = JsonConvert.DeserializeObject<List<ResultCarWithBrandsDto>>(jsonData);
//                    return View(values);
//                }
//            }

//            return View();
//        }

//        [HttpGet]
//        public async Task<IActionResult> CreateCar()
//        {
//            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
//            if (token == null) return RedirectToAction("Index", "Login");

//            var client = _httpClientFactory.CreateClient();
//            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

//            var responseMessage = await client.GetAsync("https://localhost:7003/api/Brands");

//            if (responseMessage.IsSuccessStatusCode)
//            {
//                var jsonData = await responseMessage.Content.ReadAsStringAsync();
//                var values = JsonConvert.DeserializeObject<List<ResultBrandDto>>(jsonData);

//                if (values != null)
//                {
//                    ViewBag.BrandValues = values.Select(x => new SelectListItem
//                    {
//                        Text = x.Name,
//                        Value = x.BrandID.ToString()
//                    }).ToList();

//                    return View();
//                }
//            }

//            ViewBag.BrandValues = new List<SelectListItem>();
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreateCar(CreateCarDto createCarDto)
//        {
//            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
//            var client = _httpClientFactory.CreateClient();
//            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

//            var jsonData = JsonConvert.SerializeObject(createCarDto);
//            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

//            var responseMessage = await client.PostAsync("https://localhost:7003/api/Cars", stringContent);

//            if (responseMessage.IsSuccessStatusCode)
//            {
//                return RedirectToAction("Index");
//            }

//            // ❌ Kayıt başarısızsa hata mesajını yakala
//            var errorContent = await responseMessage.Content.ReadAsStringAsync();
//            ViewBag.ApiError = errorContent;

//            // Marka listesini tekrar doldur
//            var responseBrand = await client.GetAsync("https://localhost:7003/api/Brands");
//            if (responseBrand.IsSuccessStatusCode)
//            {
//                var jsonBrandData = await responseBrand.Content.ReadAsStringAsync();
//                var values = JsonConvert.DeserializeObject<List<ResultBrandDto>>(jsonBrandData);

//                if (values != null)
//                {
//                    ViewBag.BrandValues = values.Select(x => new SelectListItem
//                    {
//                        Text = x.Name,
//                        Value = x.BrandID.ToString()
//                    }).ToList();
//                }
//            }

//            return View(createCarDto);
//        }

//        public async Task<IActionResult> RemoveCar(int id)
//        {
//            var client = _httpClientFactory.CreateClient();
//            var responseMessage = await client.DeleteAsync($"https://localhost:7003/api/Cars/{id}");
//            if (responseMessage.IsSuccessStatusCode)
//            {
//                return RedirectToAction("Index");
//            }
//            return View();
//        }

//        [HttpGet]
//        public async Task<IActionResult> UpdateCar(int id)
//        {
//            var client = _httpClientFactory.CreateClient();
//            var responseMessage1 = await client.GetAsync("https://localhost:7003/api/Brands");
//            var jsonData1 = await responseMessage1.Content.ReadAsStringAsync();
//            var values1 = JsonConvert.DeserializeObject<List<ResultBrandDto>>(jsonData1);

//            ViewBag.BrandValues = values1.Select(x => new SelectListItem
//            {
//                Text = x.Name,
//                Value = x.BrandID.ToString()
//            }).ToList();

//            var responseMessage = await client.GetAsync($"https://localhost:7003/api/Cars/{id}");
//            if (responseMessage.IsSuccessStatusCode)
//            {
//                var jsonData = await responseMessage.Content.ReadAsStringAsync();
//                var values = JsonConvert.DeserializeObject<UpdateCarDto>(jsonData);
//                return View(values);
//            }
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> UpdateCar(UpdateCarDto updateCarDto)
//        {
//            var client = _httpClientFactory.CreateClient();
//            var jsonData = JsonConvert.SerializeObject(updateCarDto);
//            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
//            var responseMessage = await client.PutAsync("https://localhost:7003/api/Cars/", stringContent);
//            if (responseMessage.IsSuccessStatusCode)
//            {
//                return RedirectToAction("Index");
//            }
//            return View();
//        }
//    }
//}


using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using UdemyCarBook.Dto.BrandDtos;
using UdemyCarBook.Dto.CarDtos;

namespace UdemyCarBook.WebUl.Controllers
{
    [Authorize(Roles = "Admin,Manger")]
    public class AdminCarController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminCarController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.GetAsync("https://localhost:7003/api/Cars/GetCarWithBrand");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultCarWithBrandsDto>>(jsonData);
                    return View(values);
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateCar()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token == null) return RedirectToAction("Index", "Login");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responseMessage = await client.GetAsync("https://localhost:7003/api/Brands");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBrandDto>>(jsonData);

                if (values != null)
                {
                    ViewBag.BrandValues = values.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.BrandID.ToString()
                    }).ToList();

                    return View();
                }
            }

            ViewBag.BrandValues = new List<SelectListItem>();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar(CreateCarDto createCarDto)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonData = JsonConvert.SerializeObject(createCarDto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://localhost:7003/api/Cars", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var errorContent = await responseMessage.Content.ReadAsStringAsync();
            ViewBag.ApiError = errorContent;

            var responseBrand = await client.GetAsync("https://localhost:7003/api/Brands");
            if (responseBrand.IsSuccessStatusCode)
            {
                var jsonBrandData = await responseBrand.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBrandDto>>(jsonBrandData);

                if (values != null)
                {
                    ViewBag.BrandValues = values.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.BrandID.ToString()
                    }).ToList();
                }
            }

            return View(createCarDto);
        }

        public async Task<IActionResult> RemoveCar(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value; // ✅ Token eklendi
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); // ✅ Header eklendi

            var responseMessage = await client.DeleteAsync($"https://localhost:7003/api/Cars/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index"); // Hata olsa da listeye dön
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCar(int id)
        {
            // 1. Token'ı alıyoruz
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // 2. Markaları çekiyoruz
            var responseMessage1 = await client.GetAsync("https://localhost:7003/api/Brands");
            var jsonData1 = await responseMessage1.Content.ReadAsStringAsync();
            var values1 = JsonConvert.DeserializeObject<List<ResultBrandDto>>(jsonData1);

            // 3. Null kontrolü (Hatanın asıl çözümü)
            if (values1 != null)
            {
                ViewBag.BrandValues = values1.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.BrandID.ToString()
                }).ToList();
            }
            else
            {
                ViewBag.BrandValues = new List<SelectListItem>();
            }

            // 4. Güncellenecek aracın mevcut bilgilerini getiriyoruz
            var responseMessage = await client.GetAsync($"https://localhost:7003/api/Cars/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateCarDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCar(UpdateCarDto updateCarDto)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonData = JsonConvert.SerializeObject(updateCarDto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // API PUT isteği (Adreste sonu slash ile bitmemeli, kontrol et kanka)
            var responseMessage = await client.PutAsync("https://localhost:7003/api/Cars", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            // Eğer hata alırsa markaları tekrar yükle ki sayfa patlamasın
            return RedirectToAction("Index");
        }
    }
}