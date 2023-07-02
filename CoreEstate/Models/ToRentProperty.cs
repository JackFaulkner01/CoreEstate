using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreEstate.Models
{
    [Table("ToRentProperties")]
    public class ToRentProperty : Property
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Rent { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Despoit { get; set; }
    }
}
