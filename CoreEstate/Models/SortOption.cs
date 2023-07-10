using System.ComponentModel.DataAnnotations;

namespace CoreEstate.Models
{
    public enum SortOption
    {
        [Display(Name = "Date oldest")]
        DateAscending,
        [Display(Name = "Date newest")]
        DateDescending,
        [Display(Name = "Price lowest")]
        PriceAscending,
        [Display(Name = "Price highest")]
        PriceDescending,
    }
}
