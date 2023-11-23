﻿using System.ComponentModel.DataAnnotations;

using DogsApp.Models.Breed;

namespace DogsApp.Models.Dog
{
    public class DogCreateViewModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [Display(Name = "Name")]
        public string Name { get; set; } = null!;
        [Range(0, 30, ErrorMessage = "Age must be between 0 and 30")]
        [Display(Name = "Age")]
        public int Age { get; set; }
        
        [Display(Name = "Breed")]
        public int BreedId { get; set; }
       // public string BreedName { get; set; } = null!;
        [Display(Name = "Dog picture")]
        public string? Picture { get; set; }
        public virtual List<BreedPairViewModel> Breeds { get; set; } = new List<BreedPairViewModel>();
 
    }
}
