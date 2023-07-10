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
using Microsoft.Data.SqlClient;

namespace CoreEstate.Pages.ToRent
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ToRentProperty> ToRentProperties { get;set; } = default!;

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

            if (_context.ToRentProperties == null)
            {
                return NotFound();
            }

            var toRentProperties = from p in _context.ToRentProperties.Include(p => p.Photos) select p;

            if (!string.IsNullOrEmpty(FilterAddress))
            {
                toRentProperties = toRentProperties.Where(p => p.Address != null && p.Address.Contains(FilterAddress));
            }

            switch (SortOption)
            {
                case Models.SortOption.PriceDescending:
                    toRentProperties = toRentProperties.OrderByDescending(p => p.Rent);
                    break;
                case Models.SortOption.PriceAscending:
                    toRentProperties = toRentProperties.OrderBy(p => p.Rent);
                    break;
                case Models.SortOption.DateDescending:
                    toRentProperties = toRentProperties.OrderByDescending(p => p.Id);
                    break;
                default:
                    toRentProperties = toRentProperties.OrderBy(p => p.Id);
                    break;
            }

            ToRentProperties = await toRentProperties.ToListAsync();

            return Page();
        }
    }
}
