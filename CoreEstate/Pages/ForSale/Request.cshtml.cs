using CoreEstate.Data;
using CoreEstate.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoreEstate.Pages.ForSale
{
    public class RequestModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<WebUser> _userManager;

        public RequestModel(ApplicationDbContext context, UserManager<WebUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

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

        [BindProperty]
        public PropertyViewing PropertyViewing { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (_context.ForSaleProperties == null || id == null)
            {
                return NotFound();
            }

            var forSaleProperty = await _context.ForSaleProperties.FindAsync(id);
            var userId = _userManager.GetUserId(User);

            if (forSaleProperty == null || userId == null)
            {
                return NotFound();
            }

            ForSaleProperty = forSaleProperty;

            if (PropertyViewing.Date < DateTime.Now)
            {
                return Page();
            }

            PropertyViewing.UserId = userId;
            PropertyViewing.ForSalePropertyId = id;
            PropertyViewing.Confirmed = false;

            _context.PropertyViewings.Add(PropertyViewing);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { id });
        }
    }
}
