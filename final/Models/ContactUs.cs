using System.ComponentModel.DataAnnotations;

namespace final.Models
{
    public class ContactUs
    {
        [Key]
        [Required(ErrorMessage = "plase enter your id")]
        public int id { get; set; }
        [Required(ErrorMessage = "plase enter your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "plase enter your Message")]
        public string Message { get; set; }
        [Required(ErrorMessage = "plase enter your Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "plase enter your Subject")]
        public string Subject { get; set; }
    }
}
