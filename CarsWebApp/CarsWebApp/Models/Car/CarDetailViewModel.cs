namespace CarsWebApp.Models.Car
{
    public class CarDetailViewModel
    {
        public int Id { get; set; }
        public string RegNumber { get; set; }
        
        public string Manufacturer { get; set; }
       
        public string Model { get; set; }
      
        public string? Picture { get; set; }
     
        public DateTime YearOfManufacture { get; set; }
      
        public double Price { get; set; }
    }
}
