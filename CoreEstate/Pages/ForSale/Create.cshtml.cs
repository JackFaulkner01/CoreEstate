using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CoreEstate.Data;
using CoreEstate.Models;

namespace CoreEstate.Pages.ForSaleProperties
{
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
        public ForSaleProperty ForSaleProperty { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.ForSaleProperties == null || ForSaleProperty == null)
            {
                return Page();
            }

            _context.ForSaleProperties.Add(ForSaleProperty);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
