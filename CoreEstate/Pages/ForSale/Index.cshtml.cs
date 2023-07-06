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
        private readonly CoreEstate.Data.ApplicationDbContext _context;

        public IndexModel(CoreEstate.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ForSaleProperty> ForSaleProperties { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.IsInRole(RoleName.IsPropertyManager))
            {
                return RedirectToPage("./Manage");
            }

            if (_context.ForSaleProperties != null)
            {
                ForSaleProperties = await _context.ForSaleProperties
                    .Include(p => p.Photos)
                    .ToListAsync();
            }

            return Page();
        }
    }
}
