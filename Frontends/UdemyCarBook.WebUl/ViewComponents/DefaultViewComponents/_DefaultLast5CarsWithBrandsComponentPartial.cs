using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UdemyCarBook.Dto.CarDtos;

namespace UdemyCarBook.WebUl.ViewComponents.DefaultViewComponents
{
    public class _DefaultLast5CarsWithBrandsComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DefaultLast5CarsWithBrandsComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();

            // 1. ADIM: Token varsa alalım ama YOKSA DA yolumuza devam edelim
            var token = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            // 2. ADIM: API'ye isteği her durumda atıyoruz
            var responseMessage = await client.GetAsync("https://localhost:7003/api/Cars/GetLast5CarWithBrand");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultLast5CarsWithBrandsDto>>(jsonData);
                return View(values);
            }

            // 3. ADIM: Eğer API başarısızsa (401 Yetkisiz hatası verirse) en azından nedenini görelim
            return View(new List<ResultLast5CarsWithBrandsDto>());
        }
    }
}