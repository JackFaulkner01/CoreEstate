using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace CoreEstate.Models
{
    public class Property
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Property Listing")]
        [StringLength(30, MinimumLength = 8)]
        [Required]
        public required string Name { get; set; }

        [StringLength(60, MinimumLength = 8)]
        [Required]
        public required string Address { get; set; }

        [Required]
        public required string Description { get; set; }

        public ICollection<PropertyPhoto> Photos { get; set; } = new Collection<PropertyPhoto>();
    }
}
