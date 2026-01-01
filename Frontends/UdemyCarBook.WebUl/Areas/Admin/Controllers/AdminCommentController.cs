using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UdemyCarBook.Dto.CommentDtos;

namespace UdemyCarBook.WebUl.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Manger")]
    [Area("Admin")]
    [Route("Admin/AdminComment")]
    public class AdminCommentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminCommentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index/{id}")]

        public async Task<IActionResult> Index(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                ViewBag.v = id;
                var responseMessange = await client.GetAsync("https://localhost:7003/api/Comments/CommentListByBlog?id=" + id);
                if (responseMessange.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessange.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(jsonData);
                    return View(values);
                }
            }
            return View();
        }
    }
}