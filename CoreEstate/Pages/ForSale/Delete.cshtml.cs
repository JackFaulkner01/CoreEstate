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
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ForSaleProperty ForSaleProperty { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ForSaleProperties == null)
            {
                return NotFound();
            }

            var forsaleproperty = await _context.ForSaleProperties.FirstOrDefaultAsync(m => m.Id == id);

            if (forsaleproperty == null)
            {
                return NotFound();
            }
            else 
            {
                ForSaleProperty = forsaleproperty;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ForSaleProperties == null)
            {
                return NotFound();
            }
            var forsaleproperty = await _context.ForSaleProperties.FindAsync(id);

            if (forsaleproperty != null)
            {
                ForSaleProperty = forsaleproperty;
                _context.ForSaleProperties.Remove(ForSaleProperty);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
