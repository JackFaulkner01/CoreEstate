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
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _host;

        public DeleteModel(ApplicationDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        [BindProperty]
      public ToRentProperty ToRentProperty { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ToRentProperties == null)
            {
                return NotFound();
            }

            var toRentPreoperty = await _context.ToRentProperties.FirstOrDefaultAsync(m => m.Id == id);

            if (toRentPreoperty == null)
            {
                return NotFound();
            }
            else 
            {
                ToRentProperty = toRentPreoperty;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ToRentProperties == null)
            {
                return NotFound();
            }

            var toRentPreoperty = await _context.ToRentProperties
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (toRentPreoperty == null)
            {
                return NotFound();
            }

            ToRentProperty = toRentPreoperty;
            var photos = ToRentProperty.Photos;
            var uploadsFolder = Path.Combine(_host.WebRootPath, "uploads");

            foreach (var photo in photos)
            {
                if (photo.Filename != null)
                {
                    System.IO.File.Delete(Path.Combine(uploadsFolder, photo.Filename));
                }
            }

            var propertyViewings = await _context.PropertyViewings.Where(p => p.ToRentPropertyId == id).ToListAsync();
            _context.PropertyViewings.RemoveRange(propertyViewings);
            _context.ToRentProperties.Remove(ToRentProperty);
            _context.PropertyPhotos.RemoveRange(photos);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
