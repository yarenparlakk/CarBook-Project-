using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers; // Bunu eklemeyi unutma!
using UdemyCarBook.Dto.TestimonialDtos;

namespace UdemyCarBook.WebUl.ViewComponents.TestimonialViewComponents
{
    public class _TestimonialComponantPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _TestimonialComponantPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // 1. Token'ı alıyoruz
            var token = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            var client = _httpClientFactory.CreateClient();

            if (token != null)
            {
                // 2. API'ye anahtarı ekliyoruz
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var responseMessange = await client.GetAsync("https://localhost:7003/api/Testimonials");
                if (responseMessange.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessange.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultTestimonialDto>>(jsonData);
                    return View(values);
                }
            }

            // 3. KRİTİK: Hata durumunda null değil, boş liste dönüyoruz
            return View(new List<ResultTestimonialDto>());
        }
    }
}