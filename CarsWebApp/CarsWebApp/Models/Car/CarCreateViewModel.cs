using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace CarsWebApp.Models.Car
{
    public class CarCreateViewModel
    {
        public int Id { get; set; }
        
        [Display(Name ="Reg number")]
        public string RegNumber { get; set; }
       
        [Display(Name = "Manufacturer")]
        public string Manufacturer { get; set; }
       
        [Display(Name = "Model")]
        public string Model { get; set; }
        [Display(Name = "Picture")]
        public string? Picture { get;set; }
        [Display(Name = "Year of Manufacture")]
        public DateTime YearOfManufacture { get; set; }
      
        [Display(Name = "Price")]
        public double Price { get; set; }
    }
}
