﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CoreEstate.Models
{
    public class WebUser : IdentityUser
    {
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public required string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? Birthdate { get; set; }
    }
}
