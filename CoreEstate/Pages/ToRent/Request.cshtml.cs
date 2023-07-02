using CoreEstate.Data;
using CoreEstate.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoreEstate.Pages.ToRent
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

        public ToRentProperty ToRentProperty { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ToRentProperties == null)
            {
                return NotFound();
            }

            var toRentProperty = await _context.ToRentProperties.FirstOrDefaultAsync(m => m.Id == id);
            
            if (toRentProperty == null)
            {
                return NotFound();
            }
            else
            {
                ToRentProperty = toRentProperty;
            }

            return Page();
        }

        [BindProperty]
        public PropertyViewing PropertyViewing { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (_context.ToRentProperties == null || id == null)
            {
                return NotFound();
            }

            var toRentProperty = await _context.ToRentProperties.FindAsync(id);
            var userId = _userManager.GetUserId(User);

            if (toRentProperty == null || userId == null)
            {
                return NotFound();
            }

            ToRentProperty = toRentProperty;

            if (PropertyViewing.Date < DateTime.Now)
            {
                return Page();
            }

            PropertyViewing.UserId = userId;
            PropertyViewing.ToRentPropertyId = id;
            PropertyViewing.Confirmed = false;

            _context.PropertyViewings.Add(PropertyViewing);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { id });
        }
    }
}
