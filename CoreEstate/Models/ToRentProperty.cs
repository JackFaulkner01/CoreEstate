using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreEstate.Models
{
    [Table("ToRentProperties")]
    public class ToRentProperty : Property
    {
        [Display(Name = "Rent Price £")]
        [Range(0, int.MaxValue)]
        [Required]
        public int Rent { get; set; }

        [Display(Name = "Rental Deposit £")]
        [Range(0, int.MaxValue)]
        [Required]
        public int Despoit { get; set; }
    }
}
