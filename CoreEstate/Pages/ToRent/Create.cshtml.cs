using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CoreEstate.Data;
using CoreEstate.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace CoreEstate.Pages.ToRent
{
    [Authorize(Roles = RoleName.IsPropertyManager)]
    public class CreateModel : PageModel
    {
        private readonly CoreEstate.Data.ApplicationDbContext _context;

        public CreateModel(CoreEstate.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ToRentProperty ToRentProperty { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.ToRentProperties == null || ToRentProperty == null)
            {
                return Page();
            }

            _context.ToRentProperties.Add(ToRentProperty);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
