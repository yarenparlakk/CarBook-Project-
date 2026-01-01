using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Dto.StatisticsDtos
{
    public class ResultStatisticsDto 
    {
        public int CarCount { get; set; }
        public int LocationCount { get; set; }
        public int AuthorCount { get; set; }
        public int BlogCount { get; set; }
        public int BrandCount { get; set; }
        public decimal avgPriceForDaily { get; set; }
        public decimal AvgRentPriceForWeekly { get; set; }
        public decimal AvgRentPriceForMonthly { get; set; }
        public int CarCountByTransmissionIsAuto { get; set; }
        public string BrandNameByMaxCar { get; set; }
        public string BlogTitleByMaxBlogComment { get; set; }
        public int CarCountByKmSmallerThen10000 { get; set; } // Bunu ekle kanka
        public int CarCountByFuelGasolineOrDiesel { get; set; }
        public int CarCountByFuelElectiric { get; set; }
        public string CarBrandAndModelByRentPriceDailyMax { get; set; }
        public string CarBrandAndModelByRentPriceDailyMin { get; set; }
    }
}
