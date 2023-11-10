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
        private readonly  ApplicationDbContext _context;
        public DogController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: DogController
        public ActionResult Index(string searchStringBreed, string searchStringName)
        {
            List<DogAllViewModel> dogs=_context.Dogs
                .Select(dogFormDb => new DogAllViewModel 
                { 
                    Id=dogFormDb.Id,
                    Name=dogFormDb.Name,
                    Age=dogFormDb.Age,
                    Breed=dogFormDb.Breed,
                    Picture=dogFormDb.Picture
                }).ToList();
            if (!String.IsNullOrEmpty(searchStringBreed) && !String.IsNullOrEmpty(searchStringName))
            {
                dogs = dogs.Where(d => d.Breed.Contains(searchStringBreed) && d.Name.Contains(searchStringName)).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringBreed))
            {
                dogs = dogs.Where(d => d.Breed.Contains(searchStringBreed)).ToList();
            }
            else if (!String.IsNullOrEmpty(searchStringName))
            {
                dogs = dogs.Where(d => d.Name.Contains(searchStringName)).ToList();
            }
            return View(dogs);
        }

        // GET: DogController/Details/5
        public ActionResult Details(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            Dog? item = _context.Dogs.Find(id);
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
                Dog dogFormDb = new Dog
                {
                    Name = bindingModel.Name,
                    Age = bindingModel.Age,
                    Breed = bindingModel.Breed,
                    Picture = bindingModel.Picture,
                };
                _context.Dogs.Add(dogFormDb);
                _context.SaveChanges();
                return this.RedirectToAction("Success");
            }
            return this.View();
        }

        // GET: DogController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            Dog? item = _context.Dogs.Find(id);
            if (item==null)
            {
                return NotFound();
            }
            DogEditViewModel dog = new DogEditViewModel()
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
        public ActionResult Edit(int id, DogEditViewModel bindingModel)
        {
            if(ModelState.IsValid)
            {
                Dog dog = new Dog
                {
                    Id = id,
                    Name = bindingModel.Name,
                    Age = bindingModel.Age,
                    Breed = bindingModel.Breed,
                    Picture = bindingModel.Picture
                };
                _context.Dogs.Update(dog);
                _context.SaveChanges();
                return this.RedirectToAction("Index");
            }
            return View(bindingModel);
        }

        // GET: DogController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            Dog? item = _context.Dogs.Find(id);
            if (item==null)
            {
                return NotFound();
            }
            DogEditViewModel dog = new DogEditViewModel()
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
        public ActionResult Delete(int id)
        {
           Dog? item=_context.Dogs.Find(id);
            if (item==null)
            {
                return NotFound();
            }
            _context.Dogs.Remove(item);
            _context.SaveChanges();
            return this.RedirectToAction("Index", "Dog");
        }
    }
}
