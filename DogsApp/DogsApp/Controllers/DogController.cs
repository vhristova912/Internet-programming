using DogsApp.Core.Contacts;
using DogsApp.Infrastructure.Data;
using DogsApp.Infrastructure.Data.Domain;
using DogsApp.Models.Dog;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace DogsApp.Controllers
{
    public class DogController : Controller
    {
        private readonly  IDogService _dogService;
        public DogController(IDogService dogService)
        {
            _dogService = dogService;
        }
        // GET: DogController
        public ActionResult Index(string searchStringBreed, string searchStringName)
        {
            List<DogAllViewModel> dogs= _dogService.GetDogs(searchStringBreed, searchStringName)
                .Select(dogFormDb => new DogAllViewModel 
                { 
                    Id=dogFormDb.Id,
                    Name=dogFormDb.Name,
                    Age=dogFormDb.Age,
                    Breed=dogFormDb.Breed,
                    Picture=dogFormDb.Picture
                }).ToList();
            return View(dogs);
        }

        // GET: DogController/Details/5
        public ActionResult Details(int id)
        {
            Dog item = _dogService.GetDogById(id);
            if (item==null)
            {
                return NotFound();
            }
            DogDetailViewModel dog = new DogDetailViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Age = item.Age,
                Breed = item.Breed,
                Picture = item.Picture
            };
            return View(dog);
        }

        // GET: DogController/Create
        public ActionResult Create()
        {
            return View();
        }
        public IActionResult Success()
        {
            return this.View();
        }
        // POST: DogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DogCreateViewModel bindingModel)
        {
            if (ModelState.IsValid)
            {
               var created=_dogService.Create(bindingModel.Name, bindingModel.Age,
                   bindingModel.Breed, bindingModel.Picture);
                if(created)
                {
                    return this.RedirectToAction("Success");
                }
            }
            return this.View();
        }

        // GET: DogController/Edit/5
        public ActionResult Edit(int id)
        {
            Dog item = _dogService.GetDogById(id);
            if (item==null)
            {
                return NotFound();
            }
            DogCreateViewModel dog = new DogCreateViewModel()
            {
                Id=item.Id,
                Name=item.Name,
                Age=item.Age,
                Breed=item.Breed,
                Picture=item.Picture
            };
            return View(dog);
        }

        // POST: DogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DogCreateViewModel bindingModel)
        {
            if(ModelState.IsValid)
            {
                var updated=_dogService.UpdateDog(id,bindingModel.Name,bindingModel.Age,
                    bindingModel.Breed, bindingModel.Picture);
                if (updated)
                {
                    return this.RedirectToAction("Index");
                }
            }
            return View(bindingModel);
        }

        // GET: DogController/Delete/5
        public ActionResult Delete(int id)
        {
            Dog item = _dogService.GetDogById(id);
            if (item==null)
            {
                return NotFound();
            }
            DogCreateViewModel dog = new DogCreateViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Age = item.Age,
                Breed = item.Breed,
                Picture = item.Picture
            };
            return View(dog);
        }

        // POST: DogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
           var deleted=_dogService.RemoveById(id);
            if (deleted)
            {
                return this.RedirectToAction("Index", "Dog");
            }
            else
            {
                return View();
            }
            
        }
    }
}
