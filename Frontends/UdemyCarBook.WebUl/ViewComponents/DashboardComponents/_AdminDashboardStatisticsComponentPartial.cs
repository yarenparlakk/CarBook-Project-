//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using UdemyCarBook.Dto.StatisticsDtos;

//namespace UdemyCarBook.WebUl.ViewComponents.DashboardComponents
//{
//    public class _AdminDashboardStatisticsComponentPartial : ViewComponent
//    {
//        private readonly IHttpClientFactory _httpClientFactory;

//        public _AdminDashboardStatisticsComponentPartial(IHttpClientFactory httpClientFactory)
//        {
//            _httpClientFactory = httpClientFactory;
//        }

//        public async Task<IViewComponentResult> InvokeAsync()
//        {
//            Random random = new Random();
//            var client = _httpClientFactory.CreateClient();









//            return View();

//        }
//    }
//}



//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using System.Net.Http.Headers; // Token için gerekli
//using UdemyCarBook.Dto.StatisticsDtos;

//namespace UdemyCarBook.WebUl.ViewComponents.DashboardComponents
//{
//    public class _AdminDashboardStatisticsComponentPartial : ViewComponent
//    {
//        private readonly IHttpClientFactory _httpClientFactory;

//        public _AdminDashboardStatisticsComponentPartial(IHttpClientFactory httpClientFactory)
//        {
//            _httpClientFactory = httpClientFactory;
//        }

//        public async Task<IViewComponentResult> InvokeAsync()
//        {
//            Random random = new Random();

//            1.Token'ı alıyoruz
//            var token = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
//            var client = _httpClientFactory.CreateClient();

//            if (!string.IsNullOrEmpty(token))
//            {
//                2.API'ye anahtarı ekliyoruz
//                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
//            }

//            #region CarCount_1
//            var responseMessange = await client.GetAsync("https://localhost:7003/api/Statistics/GetCarCount");
//            if (responseMessange.IsSuccessStatusCode)
//            {
//                int v1 = random.Next(0, 101);
//                var jsonData = await responseMessange.Content.ReadAsStringAsync();
//                var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
//                ViewBag.v = values.CarCount;
//                ViewBag.v1 = v1;
//            }
//            #endregion

//            #region CarCount_1
//            var responseMessange = await client.GetAsync("https://localhost:7003/api/Statistics/GetCarCount");
//            if (responseMessange.IsSuccessStatusCode)
//            {
//                var jsonData = await responseMessange.Content.ReadAsStringAsync();
//                var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData);
//                ViewBag.v = values.CarCount;
//            }
//            else { ViewBag.v = 0; } // Hata anında 0 yaz
//            ViewBag.v1 = random.Next(0, 101);
//            #endregion


//            #region LocationCount_2
//            var responseMessage2 = await client.GetAsync("https://localhost:7003/api/Statistics/GetLocationCount");
//            if (responseMessage2.IsSuccessStatusCode)
//            {
//                int LocationCountRandom = random.Next(0, 101);
//                var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
//                var values2 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData2);
//                ViewBag.LocationCount = values2.LocationCount;
//                ViewBag.LocationCountRandom = LocationCountRandom;
//            }
//            #endregion

//            #region LocationCount_2
//            var responseMessage2 = await client.GetAsync("https://localhost:7003/api/Statistics/GetLocationCount");
//            if (responseMessage2.IsSuccessStatusCode)
//            {
//                var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
//                var values2 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData2);
//                ViewBag.LocationCount = values2.LocationCount;
//            }
//            else { ViewBag.LocationCount = 0; }
//            ViewBag.LocationCountRandom = random.Next(0, 101);
//            #endregion

//            #region BrandCount_3
//            var responseMessage3 = await client.GetAsync("https://localhost:7003/api/Statistics/GetBrandCount");
//            if (responseMessage3.IsSuccessStatusCode)
//            {
//                int BrandCountRandom = random.Next(0, 101);
//                var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
//                var values3 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData3);
//                ViewBag.BrandCount = values3.BrandCount;
//                ViewBag.BrandCountRandom = BrandCountRandom;
//            }
//            #endregion

//            #region BrandCount_3
//            var responseMessage3 = await client.GetAsync("https://localhost:7003/api/Statistics/GetBrandCount");
//            if (responseMessage3.IsSuccessStatusCode)
//            {
//                var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
//                var values3 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData3);
//                ViewBag.BrandCount = values3.BrandCount;
//            }
//            else { ViewBag.BrandCount = 0; }
//            ViewBag.BrandCountRandom = random.Next(0, 101);
//            #endregion

//            #region AvgRentPriceForDaily_4
//            var responseMessage4 = await client.GetAsync("https://localhost:7003/api/Statistics/GetAvgRentPriceForDaily");
//            if (responseMessage4.IsSuccessStatusCode)
//            {
//                int AvgRentPriceForDailyRandom = random.Next(0, 101);
//                var jsonData4 = await responseMessage4.Content.ReadAsStringAsync();
//                var values4 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData4);
//                ViewBag.avgPriceForDaily = values4.avgPriceForDaily.ToString("0.00");
//                ViewBag.AvgRentPriceForDailyRandom = AvgRentPriceForDailyRandom;
//            }
//            #endregion

//            #region AvgRentPriceForDaily_4
//            var responseMessage4 = await client.GetAsync("https://localhost:7003/api/Statistics/GetAvgRentPriceForDaily");
//            if (responseMessage4.IsSuccessStatusCode)
//            {
//                var jsonData4 = await responseMessage4.Content.ReadAsStringAsync();
//                var values4 = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData4);
//                ViewBag.avgPriceForDaily = values4.avgPriceForDaily.ToString("0.00");
//            }
//            else { ViewBag.avgPriceForDaily = "0.00"; }
//            ViewBag.AvgRentPriceForDailyRandom = random.Next(0, 101);
//            #endregion

//            return View();
//        }
//    }
//}





using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using UdemyCarBook.Dto.StatisticsDtos;

namespace UdemyCarBook.WebUl.ViewComponents.DashboardComponents
{
    public class _AdminDashboardStatisticsComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _AdminDashboardStatisticsComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Random random = new Random();
            var client = _httpClientFactory.CreateClient();

            // 1. JWT Token Alımı
            var token = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // 2. İsimlendirme Farklarını Ortadan Kaldıran Ayar (Kritik Nokta!)
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            // --- İSTATİSTİK ÇEKME İŞLEMLERİ ---

            // Toplam Araç Sayısı
            var response1 = await client.GetAsync("https://localhost:7003/api/Statistics/GetCarCount");
            if (response1.IsSuccessStatusCode)
            {
                var jsonData = await response1.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData, settings);
                ViewBag.v = values.CarCount;
                ViewBag.v1 = random.Next(45, 98); // Grafik çubuğu için rastgele doluluk
            }

            // Lokasyon Sayısı
            var response2 = await client.GetAsync("https://localhost:7003/api/Statistics/GetLocationCount");
            if (response2.IsSuccessStatusCode)
            {
                var jsonData = await response2.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData, settings);
                ViewBag.LocationCount = values.LocationCount;
                ViewBag.LocationCountRandom = random.Next(30, 85);
            }

            // Marka Sayısı
            var response3 = await client.GetAsync("https://localhost:7003/api/Statistics/GetBrandCount");
            if (response3.IsSuccessStatusCode)
            {
                var jsonData = await response3.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData, settings);
                ViewBag.BrandCount = values.BrandCount;
                ViewBag.BrandCountRandom = random.Next(55, 95);
            }

            // Günlük Ortalama Kiralama Ücreti
            var response4 = await client.GetAsync("https://localhost:7003/api/Statistics/GetAvgRentPriceForDaily");
            if (response4.IsSuccessStatusCode)
            {
                var jsonData = await response4.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultStatisticsDto>(jsonData, settings);
                // DTO'daki "avgPriceForDaily" ismini buraya bağladık
                ViewBag.AvgPriceForDaily = values.avgPriceForDaily.ToString("0.00");
                ViewBag.AvgRentPriceForDailyRandom = random.Next(20, 75);
            }

            return View();
        }
    }
}


