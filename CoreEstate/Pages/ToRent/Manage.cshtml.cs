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
    public class ManageModel : PageModel
    {
        private readonly CoreEstate.Data.ApplicationDbContext _context;

        public ManageModel(CoreEstate.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ToRentProperty> ToRentProperties { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ToRentProperties != null)
            {
                ToRentProperties = await _context.ToRentProperties.ToListAsync();
            }
        }
    }
}
