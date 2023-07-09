using CoreEstate.Data;
using CoreEstate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoreEstate.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ForSaleProperty> ForSaleProperties { get; set; } = default!;
        public IList<ToRentProperty> ToRentProperties { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (_context.ForSaleProperties != null)
            {
                var forSaleProperties = await _context.ForSaleProperties
                    .Include(p => p.Photos)
                    .ToListAsync();
                ForSaleProperties = forSaleProperties.TakeLast(2).ToList();
            }

            if (_context.ToRentProperties != null)
            {
                var toRentProperties = await _context.ToRentProperties
                    .Include(p => p.Photos)
                    .ToListAsync();
                ToRentProperties = toRentProperties.TakeLast(2).ToList();
            }

            return Page();
        }
    }
}