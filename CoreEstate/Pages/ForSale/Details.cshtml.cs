using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoreEstate.Data;
using CoreEstate.Models;

namespace CoreEstate.Pages.ForSaleProperties
{
    public class DetailsModel : PageModel
    {
        private readonly CoreEstate.Data.ApplicationDbContext _context;

        public DetailsModel(CoreEstate.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
