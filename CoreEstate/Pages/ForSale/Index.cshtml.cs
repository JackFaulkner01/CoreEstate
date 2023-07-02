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
    public class IndexModel : PageModel
    {
        private readonly CoreEstate.Data.ApplicationDbContext _context;

        public IndexModel(CoreEstate.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ForSaleProperty> ForSaleProperty { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ForSaleProperties != null)
            {
                ForSaleProperty = await _context.ForSaleProperties.ToListAsync();
            }
        }
    }
}
