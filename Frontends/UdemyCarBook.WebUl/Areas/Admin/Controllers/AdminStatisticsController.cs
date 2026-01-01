using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UdemyCarBook.Dto.StatisticsDtos;

namespace UdemyCarBook.WebUl.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Manger")]
    [Area("Admin")]
    [Route("Admin/AdminStatistics")]
    public class AdminStatisticsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminStatisticsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]

        public async Task<IActionResult> Index()
        {
            Random random = new Random();
            var client = _httpClientFactory.CreateClient();

            #region CarCount_1
            var responseMessange = await client.GetAsync("https://localhost:7003/api/Statistics/GetCarCount");
            if (responseMessange.IsSuccessStatusCode)
            {
                int v1 = random.Next(0, 101);
                var jsonData = await responseMessange.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
                ViewBag.v = values.CarCount;
                ViewBag.v1 = v1;
            }
            #endregion

            #region LocationCount_2
            var responseMessage2 = await client.GetAsync("https://localhost:7003/api/Statistics/GetLocationCount");
            if (responseMessage2.IsSuccessStatusCode)
            {
                int LocationCountRandom = random.Next(0, 101);
                var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
                var values2 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData2);
                ViewBag.LocationCount = values2.LocationCount;
                ViewBag.LocationCountRandom = LocationCountRandom;
            }
            #endregion

            #region AuthorCount_3
            var responseMessage3 = await client.GetAsync("https://localhost:7003/api/Statistics/GetAuthorCount");
            if (responseMessage3.IsSuccessStatusCode)
            {
                int AuthorCountRandom = random.Next(0, 101);
                var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
                var values3 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData3);
                ViewBag.AuthorCount = values3.AuthorCount;
                ViewBag.AuthorCountRandom = AuthorCountRandom;
            }
            #endregion

            #region BlogCount_4
            var responseMessage4 = await client.GetAsync("https://localhost:7003/api/Statistics/GetBlogCount");
            if (responseMessage4.IsSuccessStatusCode)
            {
                int BlogCountRandom = random.Next(0, 101);
                var jsonData4 = await responseMessage4.Content.ReadAsStringAsync();
                var values4 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData4);
                ViewBag.BlogCount = values4.BlogCount;
                ViewBag.BlogCountRandom = BlogCountRandom;
            }
            #endregion

            #region BrandCount_5
            var responseMessage5 = await client.GetAsync("https://localhost:7003/api/Statistics/GetBrandCount");
            if (responseMessage5.IsSuccessStatusCode)
            {
                int BrandCountRandom = random.Next(0, 101);
                var jsonData5 = await responseMessage5.Content.ReadAsStringAsync();
                var values5 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData5);
                ViewBag.BrandCount = values5.BrandCount;
                ViewBag.BrandCountRandom = BrandCountRandom;
            }
            #endregion

            #region AvgRentPriceForDaily_6
            var responseMessage6 = await client.GetAsync("https://localhost:7003/api/Statistics/GetAvgRentPriceForDaily");
            if (responseMessage6.IsSuccessStatusCode)
            {
                int AvgRentPriceForDailyRandom = random.Next(0, 101);
                var jsonData6 = await responseMessage6.Content.ReadAsStringAsync();
                var values6 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData6);
                ViewBag.avgPriceForDaily = values6.avgPriceForDaily.ToString("0.00");
                ViewBag.AvgRentPriceForDailyRandom = AvgRentPriceForDailyRandom;
            }
            #endregion

            #region AvgRentPriceForWeekly_7
            var responseMessage7 = await client.GetAsync("https://localhost:7003/api/Statistics/GetAvgRentPriceForWeekly");
            if (responseMessage7.IsSuccessStatusCode)
            {
                int AvgRentPriceForWeeklyRandom = random.Next(0, 101);
                var jsonData7 = await responseMessage7.Content.ReadAsStringAsync();
                var values7 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData7);
                ViewBag.AvgRentPriceForWeekly = values7.AvgRentPriceForWeekly.ToString("0.00");
                ViewBag.AvgRentPriceForWeeklyRandom = AvgRentPriceForWeeklyRandom;
            }
            #endregion

            #region AvgRentPriceForMonthly_8
            var responseMessage8 = await client.GetAsync("https://localhost:7003/api/Statistics/GetAvgRentPriceForMonthly");
            if (responseMessage8.IsSuccessStatusCode)
            {
                int AvgRentPriceForMonthlyRandom = random.Next(0, 101);
                var jsonData8 = await responseMessage8.Content.ReadAsStringAsync();
                var values8 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData8);
                ViewBag.AvgRentPriceForMonthly = values8.AvgRentPriceForMonthly.ToString("0.00");
                ViewBag.AvgRentPriceForMonthlyRandom = AvgRentPriceForMonthlyRandom;
            }
            #endregion

            #region CarCountByTransmissionIsAuto_9
            var responseMessage9 = await client.GetAsync("https://localhost:7003/api/Statistics/GetCarCountByTransmissionIsAuto");
            if (responseMessage9.IsSuccessStatusCode)
            {
                int CarCountByTransmissionIsAutoRandom = random.Next(0, 101);
                var jsonData9 = await responseMessage9.Content.ReadAsStringAsync();
                var values9 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData9);
                ViewBag.CarCountByTransmissionIsAuto = values9.CarCountByTransmissionIsAuto;
                ViewBag.CarCountByTransmissionIsAutoRandom = CarCountByTransmissionIsAutoRandom;
            }
            #endregion

            #region BrandNameByMaxCar_10
            var responseMessage10 = await client.GetAsync("https://localhost:7003/api/Statistics/GetBrandNameByMaxCar");
            if (responseMessage10.IsSuccessStatusCode)
            {
                int BrandNameByMaxCarRandom = random.Next(0, 101);
                var jsonData10 = await responseMessage10.Content.ReadAsStringAsync();
                var values10 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData10);
                ViewBag.BrandNameByMaxCar = values10.BrandNameByMaxCar;
                ViewBag.BrandNameByMaxCarRandom = BrandNameByMaxCarRandom;
            }
            #endregion

            #region BlogTitleByMaxBlogComment_11
            var responseMessage11 = await client.GetAsync("https://localhost:7003/api/Statistics/GetBlogTitleByMaxBlogComment");
            if (responseMessage11.IsSuccessStatusCode)
            {
                int BlogTitleByMaxBlogCommentRandom = random.Next(0, 101);
                var jsonData11 = await responseMessage11.Content.ReadAsStringAsync();
                var values11 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData11);
                ViewBag.BlogTitleByMaxBlogComment = values11.BlogTitleByMaxBlogComment;
                ViewBag.BlogTitleByMaxBlogCommentRandom = BlogTitleByMaxBlogCommentRandom;
            }
            #endregion

            #region CarCountByKmSmallerThen10000__12
            var responseMessage12 = await client.GetAsync("https://localhost:7003/api/Statistics/GetCarCountByKmSmallerThen10000");

            if (responseMessage12.IsSuccessStatusCode)
            {
                var jsonData12 = await responseMessage12.Content.ReadAsStringAsync();

                // API sadece sayı döndürüyorsa (Örn: "8"), doğrudan int'e çeviriyoruz
                if (int.TryParse(jsonData12, out int resultCount))
                {
                    ViewBag.CarCountByKmSmallerThen10000 = resultCount;
                }
                else
                {
                    // Eğer API hâlâ nesne döndürüyorsa DTO ile güvenli bir şekilde deserialize et
                    var values12 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData12);
                    ViewBag.CarCountByKmSmallerThen10000 = values12.CarCountByKmSmallerThen10000;
                }
                ViewBag.CarCountRandom12 = random.Next(45, 95);
            }
            #endregion

            #region GetCarCountByFuelGasolineOrDiesel_13
            var responseMessage13 = await client.GetAsync("https://localhost:7003/api/Statistics/GetCarCountByFuelGasolineOrDiesel");
            if (responseMessage13.IsSuccessStatusCode)
            {
                int CarCountByFuelGasolineOrDieselRandom = random.Next(0, 101);
                var jsonData13 = await responseMessage13.Content.ReadAsStringAsync();
                var values13 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData13);
                ViewBag.CarCountByFuelGasolineOrDiesel = values13.CarCountByFuelGasolineOrDiesel;
                ViewBag.CarCountByFuelGasolineOrDieselRandom = CarCountByFuelGasolineOrDieselRandom;
            }
            #endregion

            #region GetCarCountByFuelElectiric_14
            var responseMessage14 = await client.GetAsync("https://localhost:7003/api/Statistics/GetCarCountByFuelElectiric");
            if (responseMessage14.IsSuccessStatusCode)
            {
                var jsonData14 = await responseMessage14.Content.ReadAsStringAsync();

                // Eğer API sadece "0" veya "2" döndürüyorsa direkt parse et
                if (int.TryParse(jsonData14, out int electricCount))
                {
                    ViewBag.CarCountByFuelElectiric = electricCount;
                }
                else
                {
                    // Eğer hala nesne geliyorsa DTO'yu kullan
                    var values14 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData14);
                    ViewBag.CarCountByFuelElectiric = values14.CarCountByFuelElectiric;
                }
                ViewBag.CarCountByFuelElectiricRandom = random.Next(10, 90);
            }
            #endregion

            #region GetCarBrandAndModelByRentPriceDailyMax_15
            var responseMessage15 = await client.GetAsync("https://localhost:7003/api/Statistics/GetCarBrandAndModelByRentPriceDailyMax");
            if (responseMessage15.IsSuccessStatusCode)
            {
                int CarBrandAndModelByRentPriceDailyMaxRandom = random.Next(0, 101);
                var jsonData15 = await responseMessage15.Content.ReadAsStringAsync();
                var values15 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData15);
                ViewBag.CarBrandAndModelByRentPriceDailyMax = values15.CarBrandAndModelByRentPriceDailyMax;
                ViewBag.CarBrandAndModelByRentPriceDailyMaxRandom = CarBrandAndModelByRentPriceDailyMaxRandom;
            }
            #endregion

            #region GetCarBrandAndModelByRentPriceDailyMin_16
            var responseMessage16 = await client.GetAsync("https://localhost:7003/api/Statistics/GetCarBrandAndModelByRentPriceDailyMin");
            if (responseMessage16.IsSuccessStatusCode)
            {
                int CarBrandAndModelByRentPriceDailyMinRandom = random.Next(0, 101);
                var jsonData16 = await responseMessage16.Content.ReadAsStringAsync();
                var values16 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData16);
                ViewBag.CarBrandAndModelByRentPriceDailyMin = values16.CarBrandAndModelByRentPriceDailyMin;
                ViewBag.CarBrandAndModelByRentPriceDailyMinRandom = CarBrandAndModelByRentPriceDailyMinRandom;
            }
            #endregion

            

            

            return View();

        }
    }
}
