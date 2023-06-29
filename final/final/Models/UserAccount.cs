using System.ComponentModel.DataAnnotations;

namespace final.Models
{
    public class UserAccount
    {
        [Key]
        [Required(ErrorMessage = "plase enter your id")]
        public int id { get; set; }
        [Required(ErrorMessage = "plase enter your name")]
        public string name { get; set; }
        [Required(ErrorMessage = "plase enter your password")]
        public string password { get; set; }
        [Required(ErrorMessage = "plase enter your email")]

        public string role { get; set; }
        public string email { get; set; }
    }
}
