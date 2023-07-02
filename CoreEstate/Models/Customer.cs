using System.ComponentModel.DataAnnotations;

namespace CoreEstate.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Customer Name")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public required string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [IsOver18]
        public DateTime? Birthdate { get; set; }
    }
}
