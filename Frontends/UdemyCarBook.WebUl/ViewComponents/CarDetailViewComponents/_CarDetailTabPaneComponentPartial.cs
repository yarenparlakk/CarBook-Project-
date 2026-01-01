using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UdemyCarBook.Dto.CarFeatureDtos;

namespace UdemyCarBook.WebUl.ViewComponents.CarDetailViewComponents
{
    public class _CarDetailTabPaneComponentPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var client = new HttpClient();
            // Portu senin bulduğun 7003 yaptık
            var responseMessage = await client.GetAsync($"https://localhost:7003/api/CarFeatures?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCarFeatureByCarIdDto>>(jsonData);
                return View(values);
            }
            return View(new List<ResultCarFeatureByCarIdDto>());
        }
    }
}

