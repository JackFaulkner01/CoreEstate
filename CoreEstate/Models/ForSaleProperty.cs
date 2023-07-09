using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreEstate.Models
{
    [Table("ForSaleProperties")]
    public class ForSaleProperty : Property
    {
        [Display(Name = "Sale Price £")]
        [Range(0, int.MaxValue)]
        [Required]
        public int Price { get; set; }

        [Display(Name = "Freehold Property")]
        [Required]
        public bool IsFreehold { get; set; }
    }
}
