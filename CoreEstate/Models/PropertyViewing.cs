using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreEstate.Models
{
    [Table("PropertyViewings")]
    public class PropertyViewing
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("AspNetUsers")]
        public required string UserId { get; set; }

        public WebUser? User { get; set; }

        [ForeignKey("ForSaleProperties")]
        public int? ForSalePropertyId { get; set; }

        public ForSaleProperty? ForSaleProperty { get; set; }

        [ForeignKey("ToRentProperties")]
        public int? ToRentPropertyId { get; set; }

        public ToRentProperty? ToRentProperty { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public bool Confirmed { get; set; }
    }
}
