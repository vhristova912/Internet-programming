﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DogsApp.Core.Contacts;
using DogsApp.Infrastructure.Data;
using DogsApp.Infrastructure.Data.Domain;

namespace DogsApp.Core.Services
{
    public class DogService:IDogService
    {
        private readonly ApplicationDbContext _context;
        public DogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(string name, int age, string breed, string? picture)
        {
            Dog item = new Dog()
            {
                Name = name,
                Age = age,
                Breed = breed,
                Picture = picture,
            };
            _context.Dogs.Add(item);
            return _context.SaveChanges() != 0;
        }

        public Dog GetDogById(int dogId)
        {
            return _context.Dogs.Find(dogId);
        }
        public List<Dog> GetDogs()
        {
         List<Dog> dogs=_context.Dogs.ToList();
            return dogs;
        }

        public List<Dog> GetDogs(string searchStringBreed, string searchStringName)
        {
            List<Dog> dogs = _context.Dogs.ToList();
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
            return dogs;
        }

        public bool RemoveById(int dogId)
        {
            var dog=GetDogById(dogId);
            if (dog==default(Dog))
            {
                return false;
            }
            _context.Remove(dog);
            return _context.SaveChanges() != 0;
        }

        public bool UpdateDog(int dogId, string name, int age, string breed, string? picture)
        {
            var dog = GetDogById(dogId);
            if (dog == default(Dog))
            {
                return false;
            }
            dog.Name = name;
            dog.Age = age;
            dog.Breed = breed;
            dog.Picture = picture;
            _context.Update(dog);
            return _context.SaveChanges() != 0;
           
        }
    }
}
