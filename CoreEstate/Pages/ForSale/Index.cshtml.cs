using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoreEstate.Data;
using CoreEstate.Models;
using Microsoft.AspNetCore.Authorization;

namespace CoreEstate.Pages.ForSale
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ForSaleProperty> ForSaleProperties { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? FilterAddress { get; set; }

        [BindProperty(SupportsGet = true)]
        public SortOption? SortOption { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.IsInRole(RoleName.IsPropertyManager))
            {
                return RedirectToPage("./Manage");
            }

            if (_context.ForSaleProperties == null)
            {
                return NotFound();
            }

            var forSaleProperties = from p in _context.ForSaleProperties.Include(p => p.Photos) select p;

            if (!string.IsNullOrEmpty(FilterAddress))
            {
                forSaleProperties = forSaleProperties.Where(p => p.Address != null && p.Address.Contains(FilterAddress));
            }

            switch (SortOption)
            {
                case Models.SortOption.PriceDescending:
                    forSaleProperties = forSaleProperties.OrderByDescending(p => p.Price);
                    break;
                case Models.SortOption.PriceAscending:
                    forSaleProperties = forSaleProperties.OrderBy(p => p.Price);
                    break;
                case Models.SortOption.DateDescending:
                    forSaleProperties = forSaleProperties.OrderByDescending(p => p.Id);
                    break;
                default:
                    forSaleProperties = forSaleProperties.OrderBy(p => p.Id);
                    break;
            }

            ForSaleProperties = await forSaleProperties.ToListAsync();

            return Page();
        }
    }
}
