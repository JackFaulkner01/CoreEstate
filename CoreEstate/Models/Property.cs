using System.ComponentModel.DataAnnotations;

namespace CoreEstate.Models
{
    public class Property
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 8)]
        [Required]
        public required string Name { get; set; }

        [StringLength(60, MinimumLength = 8)]
        [Required]
        public required string Address { get; set; }

        [Required]
        public required string Description { get; set; }
    }
}
