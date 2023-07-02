using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreEstate.Data;
using CoreEstate.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace CoreEstate.Pages.ForSale
{
    [Authorize(Roles = RoleName.IsPropertyManager)]
    public class EditModel : PageModel
    {
        private readonly CoreEstate.Data.ApplicationDbContext _context;

        public EditModel(CoreEstate.Data.ApplicationDbContext context)
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

            var forsaleproperty =  await _context.ForSaleProperties.FirstOrDefaultAsync(m => m.Id == id);
            if (forsaleproperty == null)
            {
                return NotFound();
            }
            ForSaleProperty = forsaleproperty;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ForSaleProperty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ForSalePropertyExists(ForSaleProperty.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ForSalePropertyExists(int id)
        {
          return (_context.ForSaleProperties?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
