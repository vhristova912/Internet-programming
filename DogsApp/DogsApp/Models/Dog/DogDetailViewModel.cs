﻿namespace DogsApp.Models.Dog
{
    public class DogDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get;set; }
        public string BreedName { get; set; } = null!;
        public string? Picture { get; set; }
        public string FullName { get; set; } = null!;
    }
}
