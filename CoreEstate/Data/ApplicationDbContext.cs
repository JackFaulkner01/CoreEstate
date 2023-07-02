using CoreEstate.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreEstate.Data
{
    public class ApplicationDbContext : IdentityDbContext<WebUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ForSaleProperty> ForSaleProperties { get; set; } = default!;

        public DbSet<ToRentProperty> ToRentProperties { get; set; } = default!;

        public DbSet<PropertyViewing> PropertyViewings { get; set; } = default!;
    }
}