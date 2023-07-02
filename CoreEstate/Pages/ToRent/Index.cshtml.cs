using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoreEstate.Data;
using CoreEstate.Models;

namespace CoreEstate.Pages.ToRentProperties
{
    public class IndexModel : PageModel
    {
        private readonly CoreEstate.Data.ApplicationDbContext _context;

        public IndexModel(CoreEstate.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ToRentProperty> ToRentProperty { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ToRentProperties != null)
            {
                ToRentProperty = await _context.ToRentProperties.ToListAsync();
            }
        }
    }
}
