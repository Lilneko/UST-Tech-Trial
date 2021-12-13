using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace USTTechTrial.Models
{
    public class Order
    {
        [Key]
        [Required(ErrorMessage = "The variable orderId is Required")]
        [MaxLength(10, ErrorMessage = "Maximum length of 10")]
        public string orderId { get; set; }

        public List<Item> items { get; set; }
        public decimal total { get; set; }
    }
}
