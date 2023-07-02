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
using System.Data;

namespace CoreEstate.Pages.ForSale
{
    [Authorize(Roles = RoleName.IsPropertyManager)]
    public class ViewingsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ViewingsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<PropertyViewing> PropertyViewings { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PropertyViewings == null)
            {
                return NotFound();
            }

            if (_context.PropertyViewings != null)
            {
                PropertyViewings = await _context.PropertyViewings
                    .Where(v => v.ForSalePropertyId == id)
                    .Include(v => v.ForSaleProperty)
                    .Include(v => v.User)
                    .ToListAsync();
            }

            return Page();
        }
    }
}
