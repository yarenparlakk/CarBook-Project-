using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UdemyCarBook.Dto.CarFeatureDtos;
using UdemyCarBook.Dto.FeatureDtos;

[Authorize(Roles = "Admin,Manger")]
[Area("Admin")]
// Sınıfın üzerindeki Route'u kaldırıp metotlara tam yol verelim
public class AdminCarFeatureDetailController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    public AdminCarFeatureDetailController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    // TAM YOL: localhost:xxxx/Admin/AdminCarFeatureDetail/Index/24
    [HttpGet]
    [Route("Admin/AdminCarFeatureDetail/Index/{id}")]
    public async Task<IActionResult> Index(int id)
    {
        var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
        var client = _httpClientFactory.CreateClient();

        if (!string.IsNullOrEmpty(token))
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // API URL'ini tam kontrol et, "ByCarId" kısmından sonra "id=" parametresi mi bekliyor yoksa "/id" mi?
        var responseMessage = await client.GetAsync($"https://localhost:7003/api/CarFeatures/GetCarFeatureByCarIdListByCarId?id={id}");

        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCarFeatureByCarIdDto>>(jsonData);
            return View(values);
        }
        return View(new List<ResultCarFeatureByCarIdDto>());
    }

    [HttpPost]
    [Route("Admin/AdminCarFeatureDetail/Index/{id}")]
    public async Task<IActionResult> Index(List<ResultCarFeatureByCarIdDto> model)
    {
        var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        foreach (var item in model)
        {
            if (item.Available)
                await client.GetAsync($"https://localhost:7003/api/CarFeatures/UpdateCarFeatureAvailableToTrue/{item.CarFeatureID}");
            else
                await client.GetAsync($"https://localhost:7003/api/CarFeatures/UpdateCarFeatureAvailableToFalse/{item.CarFeatureID}");
        }
        return RedirectToAction("Index", "AdminCar", new { area = "Admin" });
    }

    // 3. YENİ ÖZELLİK TANIMLAMA SAYFASI (Opsiyonel)
    [HttpGet]
    [Route("CreateFeature")] // URL: Admin/AdminCarFeatureDetail/CreateFeature
    public IActionResult CreateFeature()
    {
        return View(new CreateFeatureDto());
    }
}


