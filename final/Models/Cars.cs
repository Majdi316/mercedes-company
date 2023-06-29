using System.ComponentModel.DataAnnotations;

namespace final.Models
{
    public class Cars
    {
        [Key]
        [Required(ErrorMessage = "plase enter id of the car")]
        public int id { get; set; }
        [Required(ErrorMessage = "plase enter name of the car")]
        public string Car_Name { get; set; }
        [Required(ErrorMessage = "plase enter price of the car")]
        public string Price { get; set; }
        [Required(ErrorMessage = "plase enter Color of the car")]
        public string Color { get; set; }
        [Required(ErrorMessage = "plase enter Passenger_Capacity of the car")]
        public string Passenger_Capacity { get; set; }
        [Required(ErrorMessage = "plase enter Engine of the car")]
        public string Engine { get; set; }
        [Required(ErrorMessage = "plase enter Power of the car")]
        public string Power { get; set; }
        [Required(ErrorMessage = "plase enter Automatic_Transmission of the car")]
        public string Automatic_Transmission { get; set; }
        [Required(ErrorMessage = "plase enter Cargo_Capacity of the car")]
        public string Cargo_Capacity { get; set; }
        [Required(ErrorMessage = "plase enter Acceleration of the car")]
        public string Acceleration { get; set; }
        [Required(ErrorMessage = "plase enter City_Fuel_Economy of the car")]
        public string City_Fuel_Economy { get; set; }
        [Required(ErrorMessage = "plase enter High_Fuel_Economy of the car")]
        public string High_Fuel_Economy { get; set; }
        [Required(ErrorMessage = "plase enter Image_File of the car")]
        public string Image_File { get; set; }
    }
}
