using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreEstate.Models
{
    [Table("ForSaleProperties")]
    public class ForSaleProperty : Property
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Price { get; set; }

        [Required]
        public bool IsFreehold { get; set; }
    }
}
