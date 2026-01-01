using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UdemyCarBook.Dto.FooterAddressDtos;

namespace UdemyCarBook.WebUl.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Manger")]
    [Area("Admin")]
    [Route("Admin/AdminFooterAddress")]
    public class AdminFooterAddressController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminFooterAddressController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]

        public async Task<IActionResult> Index()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseMessange = await client.GetAsync("https://localhost:7003/api/FooterAddresses");
                if (responseMessange.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessange.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultFooterAddressDto>>(jsonData);
                    return View(values);
                }
            }
            
            return View();
        }


        [Route("RemoveFooterAddress/{id}")]

        public async Task<IActionResult> RemoveFooterAddress(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync("https://localhost:7003/api/FooterAddresses?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminFooterAddress", new { area = "Admin" });
            }
            return View();
        }

        [HttpGet]

        [Route("UpdateFooterAddress/{id}")]

        public async Task<IActionResult> UpdateFooterAddress(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7003/api/FooterAddresses/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateFooterAddressDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        [Route("UpdateFooterAddress/{id}")]
        public async Task<IActionResult> UpdateFooterAddress(int id, UpdateFooterAddressDto updateFooterAddressDto)
        {
            // View'den id gelmiyorsa burada set ediliyor
            updateFooterAddressDto.FooterAddressID = id;

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateFooterAddressDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // PUT URL'e id ekle
            var responseMessage = await client.PutAsync(
                $"https://localhost:7003/api/FooterAddresses/{id}",
                stringContent
            );

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminFooterAddress", new { area = "Admin" });
            }

            return View(updateFooterAddressDto);
        }

    }
}
