
using CarApp.Core.Contracts;

using CarsApp.Infrastructure.Data.Domain;

using CarsWebApp.Infrastructure.Data;
using CarsWebApp.Models.Car;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace CarsWebApp.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        // GET: CarController
        public ActionResult Index(string searchStringModel, string searchDoublePrice)
        {
            List<CarAllViewModel> cars = _carService.GetCars( searchStringModel, searchDoublePrice)
              .Select(carFormDb => new CarAllViewModel
              {
                  Id=carFormDb.Id,
                  RegNumber=carFormDb.RegNumber,
                  Manufacturer=carFormDb.Manufacturer,
                  Model = carFormDb.Model,
                  Picture=carFormDb.Picture,
                  YearOfManufacture=carFormDb.YearOfManufacture,
                  Price=carFormDb.Price,
              }).ToList();
            return View(cars);
        }

        // GET: CarController/Details/5
        public ActionResult Details(int id)
        {
            Car item = _carService.GetCarById(id);
            if (item == null)
            {
                return NotFound();
            }
           CarDetailViewModel car = new CarDetailViewModel()
            {
               Id = item.Id,
               RegNumber = item.RegNumber,
               Manufacturer = item.Manufacturer,
               Model = item.Model,
               Picture = item.Picture,
               YearOfManufacture = item.YearOfManufacture,
               Price = item.Price,
           };
            return View(car);
        }

        // GET: CarController/Create
        public ActionResult Create()
        {
            return View();
        }
        public IActionResult Success()
        {
            return this.View();
        }

        // POST: CarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarCreateViewModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                var created = _carService.Create(bindingModel.RegNumber, bindingModel.Manufacturer,
                    bindingModel.Model, bindingModel.Picture, bindingModel.YearOfManufacture, bindingModel.Price);
                if (created)
                {
                    return this.RedirectToAction("Success");
                }
            }
            return this.View();
        }

        // GET: CarController/Edit/5
        public ActionResult Edit(int id)
        {
            Car item = _carService.GetCarById(id);
            if (item == null)
            {
                return NotFound();
            }
            CarEditViewModel car = new CarEditViewModel()
            {
                Id = item.Id,
                RegNumber = item.RegNumber,
                Manufacturer = item.Manufacturer,
                Model = item.Model,
                Picture = item.Picture,
                YearOfManufacture = item.YearOfManufacture,
                Price = item.Price,
            };
            return View(car);
        }

        // POST: CarController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CarCreateViewModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                var updated = _carService.UpdateCar(id, bindingModel.RegNumber, bindingModel.Manufacturer,
                    bindingModel.Model, bindingModel.Picture, bindingModel.YearOfManufacture, bindingModel.Price);
                if (updated)
                {
                    return this.RedirectToAction("Index");
                }
            }
            return View(bindingModel);
        }

        // GET: CarController/Delete/5
        public ActionResult Delete(int id)
        {
            Car item = _carService.GetCarById(id);
            if (item == null)
            {
                return NotFound();
            }
            CarDetailViewModel car = new CarDetailViewModel()
            {
                Id = item.Id,
                RegNumber = item.RegNumber,
                Manufacturer = item.Manufacturer,
                Model = item.Model,
                Picture = item.Picture,
                YearOfManufacture = item.YearOfManufacture,
                Price = item.Price,
            };
            return View(car);
        }

        // POST: CarController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var deleted = _carService.RemoveById(id);
            if (deleted)
            {
                return this.RedirectToAction("Index", "Car");
            }
            else
            {
                return View();
            }
        }
    }
}
