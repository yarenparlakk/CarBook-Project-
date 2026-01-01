using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers; // Token için ekledik
using UdemyCarBook.Dto.StatisticsDtos;

namespace UdemyCarBook.WebUl.ViewComponents.DefaultViewComponents
{
    public class _DefaultStatisticsComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DefaultStatisticsComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // 1. Token'ı alıyoruz
            var token = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            var client = _httpClientFactory.CreateClient();

            if (!string.IsNullOrEmpty(token))
            {
                // 2. Token'ı Header'a ekliyoruz (Tüm isteklerde geçerli olur)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            #region CarCount_1
            var responseMessange = await client.GetAsync("https://localhost:7003/api/Statistics/GetCarCount");
            if (responseMessange.IsSuccessStatusCode)
            {
                var jsonData = await responseMessange.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
                ViewBag.carCount = values.CarCount;
            }
            #endregion

            #region LocationCount_2
            var responseMessage2 = await client.GetAsync("https://localhost:7003/api/Statistics/GetLocationCount");
            if (responseMessage2.IsSuccessStatusCode)
            {
                var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
                var values2 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData2);
                ViewBag.LocationCount = values2.LocationCount;
            }
            #endregion

            #region BrandCount_3
            var responseMessage3 = await client.GetAsync("https://localhost:7003/api/Statistics/GetBrandCount");
            if (responseMessage3.IsSuccessStatusCode)
            {
                var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
                var values3 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData3);
                ViewBag.BrandCount = values3.BrandCount;
            }
            #endregion

            #region GetCarCountByFuelElectiric_4
            var responseMessage4 = await client.GetAsync("https://localhost:7003/api/Statistics/GetCarCountByFuelElectiric");
            if (responseMessage4.IsSuccessStatusCode)
            {
                var jsonData4 = await responseMessage4.Content.ReadAsStringAsync();
                var values4 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData4);
                ViewBag.CarCountByFuelElectiric = values4.CarCountByFuelElectiric;
            }
            #endregion

            return View();
        }
    }
}

