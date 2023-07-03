using CoreEstate.Data;
using CoreEstate.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoreEstate.Pages
{
    public class ViewingsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<WebUser> _userManager;

        public ViewingsModel(ApplicationDbContext context, UserManager<WebUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<PropertyViewing> PropertyViewings { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (_context.PropertyViewings == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return NotFound();
            }

            PropertyViewings = await _context.PropertyViewings
                .Where(v => v.UserId == userId)
                .Include(v => v.ForSaleProperty)
                .Include(v => v.ToRentProperty)
                .ToListAsync();

            return Page();
        }
    }
}
