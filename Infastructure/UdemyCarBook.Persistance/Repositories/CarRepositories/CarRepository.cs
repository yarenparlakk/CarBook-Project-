using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UdemyCarBook.Application.Interfaces.CarInterfaces;
using UdemyCarBook.Domain.Entities;
using UdemyCarBook.Persistance.Context;

namespace UdemyCarBook.Persistance.Repositories.CarRepositories
{
    public class CarRepository: ICarRepository
    {
        private readonly CarBookContext _context;

        public CarRepository(CarBookContext context)
        {
            _context = context;
        }

        public int GetCarCount()
        {
            var value = _context.Cars.Count();
            return value;
        }

        public List<Car> GetCarsListWithBrands()
        {
            var values = _context.Cars.Include(x => x.Brand).ToList();
            return values;
        }

        public List<Car> GetLast5CarsWithBrands()
        {
            var values = _context.Cars
               .Include(x => x.Brand)              // Önce markayı al
               .Include(x => x.CarPricings)        // Sonra aracın tüm fiyatlarını al
               .ThenInclude(y => y.Pricing)        // Fiyatın türünü (Günlük/Haftalık) al
               .OrderByDescending(x => x.CarID)
               .Take(5)
               .ToList();
            return values;
        }
    }
}
