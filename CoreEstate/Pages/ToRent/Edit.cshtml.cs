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

namespace CoreEstate.Pages.ToRentProperties
{
    public class EditModel : PageModel
    {
        private readonly CoreEstate.Data.ApplicationDbContext _context;

        public EditModel(CoreEstate.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ToRentProperty ToRentProperty { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ToRentProperties == null)
            {
                return NotFound();
            }

            var torentproperty =  await _context.ToRentProperties.FirstOrDefaultAsync(m => m.Id == id);
            if (torentproperty == null)
            {
                return NotFound();
            }
            ToRentProperty = torentproperty;
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

            _context.Attach(ToRentProperty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToRentPropertyExists(ToRentProperty.Id))
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

        private bool ToRentPropertyExists(int id)
        {
          return (_context.ToRentProperties?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
