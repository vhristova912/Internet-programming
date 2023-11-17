using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarApp.Core.Contracts;

using CarsApp.Infrastructure.Data.Domain;

using CarsWebApp.Infrastructure.Data;

namespace CarApp.Core.Services
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _context;
        public CarService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(string regNumber, string manufacturer, string model, string? picture, DateTime yearOfManufacture, double price)
        {
            Car item = new Car
            {
                RegNumber = regNumber,
                Manufacturer = manufacturer,
                Model = model,
                Picture = picture,
                YearOfManufacture = yearOfManufacture,
                Price = price,
            };
            _context.Cars.Add(item);
            return _context.SaveChanges() != 0;
        }

        public Car GetCarById(int carId)
        {
            return _context.Cars.Find(carId);
        }

        public List<Car> GetCars()
        {
            List<Car> cars = _context.Cars.ToList();
            return cars;
        }

        public List<Car> GetCars(string searchStringModel, string searchDoublePrice)
        {
            List<Car> cars=_context.Cars.ToList();
            if (!String.IsNullOrEmpty(searchDoublePrice) && !String.IsNullOrEmpty(searchStringModel))
            {
                cars = cars.Where(d => d.Model.Contains(searchStringModel) && d.Model.Contains(searchDoublePrice)).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringModel))
            {
                cars = cars.Where(d => d.Model.Contains(searchStringModel)).ToList();
            }
            else if(!String.IsNullOrEmpty(searchDoublePrice)) 
            {
                cars = cars.Where(d => d.Price.ToString().Contains(searchDoublePrice)).ToList();
            }
            return cars;

        }

        public List<Car> GetCars(string searchStringModel, double searchDoublePrice)
        {
            throw new NotImplementedException();
        }

        public bool RemoveById(int carId)
        {
            var car=GetCarById(carId);
            if(car==default(Car))
            {
                return false;
            }
            _context.Remove(car);
            return _context.SaveChanges() != 0;
        }

        public bool UpdateCar(int carId, string regNumber, string manufacturer, string model, string picture, DateTime yearOfManufacture, double price)
        {
            var car = GetCarById(carId);
            if (car == default(Car))
            {
                return false;
            }
            car.RegNumber= regNumber;
            car.Manufacturer=manufacturer;
            car.Model= model;
            car.Picture= picture;
            car.YearOfManufacture = yearOfManufacture;
            car.Price= price;
            _context.Update(car);
            return _context.SaveChanges() != 0;
        }
    }
}
