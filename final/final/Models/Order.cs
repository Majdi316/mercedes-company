using System;
using System.ComponentModel.DataAnnotations;

namespace final.Models
{
    public class Order
    {
        [Key]
        public int id { get; set; }
        public Cars car { get; set; }
        public UserAccount user { get; set; }
        public int quantity { get; set; }
        public DateTime dateTime { get; set; }
    }
}
