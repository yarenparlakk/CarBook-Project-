using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCarBook.Dto.RentACarDtos
{
    public class FilterRentACarDto
    {
        //public int locationID { get; set; }
        //public bool available { get; set; }
        public int CarID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Amount { get; set; }
        public string CoverImageUrl { get; set; }
        public decimal DailyAmount { get; set; } // Bu değer API'den gelmiyor olabilir
    }
}
