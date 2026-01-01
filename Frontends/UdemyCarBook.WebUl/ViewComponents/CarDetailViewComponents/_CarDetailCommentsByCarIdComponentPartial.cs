using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers; // Token Header'ı için mutlaka ekle
using UdemyCarBook.Dto.ReviewDtos;

namespace UdemyCarBook.WebUl.ViewComponents.CarDetailViewComponents
{
    public class _CarDetailCommentsByCarIdComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _CarDetailCommentsByCarIdComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.CarId = id;

            // 1. Token'ı Claim'lerden çekiyoruz
            var token = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            var client = _httpClientFactory.CreateClient();

            if (!string.IsNullOrEmpty(token))
            {
                // 2. API'ye token'ı ekleyerek yetkili olduğumuzu bildiriyoruz
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseMessage = await client.GetAsync("https://localhost:7003/api/Reviews?id=" + id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultReviewByCarIdDto>>(jsonData);
                    return View(values);
                }
            }

            // 3. KRİTİK: Hata anında veya token yoksa BOŞ LİSTE dönüyoruz
            // Bu sayede yorumlar henüz yoksa bile sayfa patlamayacak.
            return View(new List<ResultReviewByCarIdDto>());
        }
    }
}

