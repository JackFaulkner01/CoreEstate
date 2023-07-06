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
        private readonly IWebHostEnvironment _host;

        public DeleteModel(ApplicationDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        [BindProperty]
        public ForSaleProperty ForSaleProperty { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ForSaleProperties == null)
            {
                return NotFound();
            }

            var forSaleProperty = await _context.ForSaleProperties.FirstOrDefaultAsync(m => m.Id == id);

            if (forSaleProperty == null)
            {
                return NotFound();
            }
            else
            {
                ForSaleProperty = forSaleProperty;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ForSaleProperties == null)
            {
                return NotFound();
            }

            var forSaleProperty = await _context.ForSaleProperties
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (forSaleProperty == null)
            {
                return NotFound();
            }

            ForSaleProperty = forSaleProperty;
            var photos = ForSaleProperty.Photos;
            var uploadsFolder = Path.Combine(_host.WebRootPath, "uploads");

            foreach (var photo in photos)
            {
                if (photo.Filename != null)
                {
                    System.IO.File.Delete(Path.Combine(uploadsFolder, photo.Filename));
                }
            }

            var propertyViewings = await _context.PropertyViewings.Where(p => p.ForSalePropertyId == id).ToListAsync();
            _context.PropertyViewings.RemoveRange(propertyViewings);
            _context.ForSaleProperties.Remove(ForSaleProperty);
            _context.PropertyPhotos.RemoveRange(photos);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
