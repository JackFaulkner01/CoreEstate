using CoreEstate.Data;
using CoreEstate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CoreEstate.Pages.ToRent
{
    [Authorize(Roles = RoleName.IsPropertyManager)]
    public class ConfirmViewingModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ConfirmViewingModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PropertyViewing PropertyViewing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PropertyViewings == null)
            {
                return NotFound();
            }

            var propertyViewing = await _context.PropertyViewings
                .Include(v => v.ToRentProperty)
                .Include(v => v.User)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (propertyViewing == null)
            {
                return NotFound();
            }

            PropertyViewing = propertyViewing;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (_context.PropertyViewings == null || id == null)
            {
                return Page();
            }

            var propertyViewing = await _context.PropertyViewings
                .FirstOrDefaultAsync(v => v.Id == id);

            if (propertyViewing == null)
            {
                return NotFound();
            }

            propertyViewing.Confirmed = PropertyViewing.Confirmed;
            _context.Attach(propertyViewing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyViewingExists(propertyViewing.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Viewings", new { id = propertyViewing.ToRentPropertyId });
        }

        private bool PropertyViewingExists(int id)
        {
            return (_context.PropertyViewings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
