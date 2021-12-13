using System;
using System.ComponentModel.DataAnnotations;

namespace USTTechTrial.Models
{    
    public class Item
    {
        [Key]
        [Required(ErrorMessage = "The variable code is Required")]
        [MaxLength(10, ErrorMessage = "Maximum length of 10")]
        public string code { get; set; }

        [Required(ErrorMessage = "The variable units is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "The value must be greater than zero")]
        public int units { get; set; }

        [Required(ErrorMessage = "The variable pricePerUnit is Required")]
        [RegularExpression(@"[0-9][, ]*[0-9]{0,2}")]
        public decimal pricePerUnit { get; set; }

        [Range(1, 2, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int type { get; set; } = 1;


        public decimal subTotal { get; set; }
        public decimal vatPercentage { get; set; }
        public decimal totalWithVat { get; set; }

    }
}
   
   
   
   
   
