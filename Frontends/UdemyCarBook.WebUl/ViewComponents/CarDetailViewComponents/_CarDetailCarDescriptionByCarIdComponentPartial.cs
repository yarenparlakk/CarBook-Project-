using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers; // Token Header'ı için mutlaka ekle
using UdemyCarBook.Dto.CarDescriptionDtos;

namespace UdemyCarBook.WebUl.ViewComponents.CarDetailViewComponents
{
    public class _CarDetailCarDescriptionByCarIdComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _CarDetailCarDescriptionByCarIdComponentPartial(IHttpClientFactory httpClientFactory)
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
                // 2. API'ye token'ı ekleyerek yetki alıyoruz
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseMessage = await client.GetAsync($"https://localhost:7003/api/CarDescriptions?id=" + id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<ResultCarDescriptionByCarIdDto>(jsonData);
                    return View(values);
                }
            }

            // 3. KRİTİK: Hata anında null değil, BOŞ BİR NESNE dönüyoruz
            // Bu sayede View içindeki @Model.Description gibi alanlar patlamayacak.
            return View(new ResultCarDescriptionByCarIdDto());
        }
    }
}