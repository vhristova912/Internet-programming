using System.ComponentModel.DataAnnotations;

namespace CarsWebApp.Models.Car
{
    public class CarEditViewModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(8)]
        [Display(Name = "Reg number")]
        public string RegNumber { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name = "Manufacturer")]
        public string Manufacturer { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name = "Model")]
        public string Model { get; set; }
        [Display(Name = "Picture")]
        public string? Picture { get; set; }
        [Display(Name = "Year of Manufacture")]
        public DateTime YearOfManufacture { get; set; }
        [Required]
        [Range(1000, 300000)]
        [Display(Name = "Price")]
        public double Price { get; set; }
    }
}
