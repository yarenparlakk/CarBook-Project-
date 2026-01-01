using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers; // Token Header'ı için mutlaka ekle
using UdemyCarBook.Dto.CarFeatureDtos;

namespace UdemyCarBook.WebUl.ViewComponents.CarDetailViewComponents
{
    public class _CarDetailCarFeatureByCarIdComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _CarDetailCarFeatureByCarIdComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.CarId = id;

            // 1. Giriş anahtarını (Token) claim'lerden çekiyoruz
            var token = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            var client = _httpClientFactory.CreateClient();

            if (!string.IsNullOrEmpty(token))
            {
                // 2. API'ye "Ben geldim, işte anahtarım" diyerek token'ı ekliyoruz
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseMessage = await client.GetAsync("https://localhost:7003/api/CarFeatures?id=" + id);
                // Örnek: _CarDetailCarFeatureByCarIdComponentPartial.cs
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultCarFeatureByCarIdDto>>(jsonData);
                    return View(values);
                }

                // BURASI ÇOK KRİTİK: Liste bekleyen yerlerde boş liste dönmelisin kanka!
                return View(new List<ResultCarFeatureByCarIdDto>());
            }

            // 3. KRİTİK: Hata anında veya token yoksa null değil, BOŞ LİSTE dönüyoruz
            // Bu sayede Razor tarafındaki @foreach (var item in Model) hatası tarihe karışacak.
            return View(new List<ResultCarFeatureByCarIdDto>());
        }
    }
}