using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UdemyCarBook.Dto.TagCloudDtos; // DTO hatasını çözer
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;


namespace UdemyCarBook.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Manger")]
    [Area("Admin")]
    [Route("Admin/AdminTagCloud")]
    public class AdminTagCloudController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory; // Tanımsız hata (CS0103) çözümü

        public AdminTagCloudController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7003/api/TagClouds");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                // Sınıf adını burada güncelledik
                var values = JsonConvert.DeserializeObject<List<GetByBlogIdTagCloudDto>>(jsonData);
                return View(values);
            }
            return View(new List<GetByBlogIdTagCloudDto>());
        }
    }
}