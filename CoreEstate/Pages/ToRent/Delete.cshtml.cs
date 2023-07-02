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

namespace CoreEstate.Pages.ToRent
{
    [Authorize(Roles = RoleName.IsPropertyManager)]
    public class DeleteModel : PageModel
    {
        private readonly CoreEstate.Data.ApplicationDbContext _context;

        public DeleteModel(CoreEstate.Data.ApplicationDbContext context)
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

            var torentproperty = await _context.ToRentProperties.FirstOrDefaultAsync(m => m.Id == id);

            if (torentproperty == null)
            {
                return NotFound();
            }
            else 
            {
                ToRentProperty = torentproperty;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ToRentProperties == null)
            {
                return NotFound();
            }
            var torentproperty = await _context.ToRentProperties.FindAsync(id);

            if (torentproperty != null)
            {
                ToRentProperty = torentproperty;
                _context.ToRentProperties.Remove(ToRentProperty);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
