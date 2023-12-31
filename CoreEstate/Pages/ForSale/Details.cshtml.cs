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

namespace CoreEstate.Pages.ForSale
{
    [AllowAnonymous]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
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

            var forSaleProperty = await _context.ForSaleProperties
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (forSaleProperty == null)
            {
                return NotFound();
            }

            ForSaleProperty = forSaleProperty;
            
            return Page();
        }
    }
}
