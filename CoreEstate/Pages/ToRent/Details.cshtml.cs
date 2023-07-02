﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoreEstate.Data;
using CoreEstate.Models;
using Microsoft.AspNetCore.Authorization;

namespace CoreEstate.Pages.ToRent
{
    [AllowAnonymous]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
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
    }
}
