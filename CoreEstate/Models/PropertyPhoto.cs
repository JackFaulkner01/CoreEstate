using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreEstate.Models
{
    [Table("PropertyPhotos")]
    public class PropertyPhoto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string? Filename { get; set; }
    }
}
