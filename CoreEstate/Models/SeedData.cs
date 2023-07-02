using CoreEstate.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoreEstate.Models
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context == null 
                    || context.Users == null)
                {
                    throw new ArgumentNullException("Null ApplicationDbContext.");
                }

                // Look for any users.
                if (!context.Users.Any())
                {
                    // Database has not been seeded with users.
                    context.Users.AddRange(
                        new WebUser { UserName = "husna.johns@coreestate.com", Email = "husna.johns@coreestate.com", Name = "Husna Johns", Birthdate = DateTime.Parse("1975-09-24") },
                        new WebUser { UserName = "dennis.hall@coreestate.com", Email = "dennis.hall@coreestate.com", Name = "Dennis Hall", Birthdate = DateTime.Parse("1968-12-01") },
                        new WebUser { UserName = "ebony.obrien@coreestate.com", Email = "ebony.obrien@coreestate.com", Name = "Ebony Obrien", Birthdate = DateTime.Parse("1958-11-11") },
                        new WebUser { UserName = "hugo.dalton@coreestate.com", Email = "hugo.dalton@coreestate.com", Name = "Hugo Dalton", Birthdate = DateTime.Parse("1983-11-19") },
                        new WebUser { UserName = "lochlan.bruce@coreestate.com", Email = "lochlan.bruce@coreestate.com", Name = "Lochlan Bruce", Birthdate = DateTime.Parse("1976-10-19") },
                        new WebUser { UserName = "gloria.flores@coreestate.com", Email = "gloria.flores@coreestate.com", Name = "Gloria Flores", Birthdate = DateTime.Parse("1982-11-19") },
                        new WebUser { UserName = "arron.haley@coreestate.com", Email = "arron.haley@coreestate.com", Name = "Arron Haley", Birthdate = DateTime.Parse("1951-07-06") },
                        new WebUser { UserName = "kamil.peterson@coreestate.com", Email = "kamil.peterson@coreestate.com", Name = "Kamil Peterson", Birthdate = DateTime.Parse("1962-03-22") },
                        new WebUser { UserName = "jaxon.leon@coreestate.com", Email = "jaxon.leon@coreestate.com", Name = "Jaxon Leon", Birthdate = DateTime.Parse("1969-10-15") },
                        new WebUser { UserName = "rita.bush@coreestate.com", Email = "rita.bush@coreestate.com", Name = "Rita Bush", Birthdate = DateTime.Parse("1982-01-23") },
                        new WebUser { UserName = "livia.horne@coreestate.com", Email = "livia.horne@coreestate.com", Name = "Livia Horne", Birthdate = DateTime.Parse("1994-11-15") },
                        new WebUser { UserName = "zaki.mckinney@coreestate.com", Email = "zaki.mckinney@coreestate.com", Name = "Zaki Mckinney", Birthdate = DateTime.Parse("1959-02-07") },
                        new WebUser { UserName = "gail.carey@coreestate.com", Email = "gail.carey@coreestate.com", Name = "Gail Carey", Birthdate = DateTime.Parse("1961-04-27") },
                        new WebUser { UserName = "mahdi.wang@coreestate.com", Email = "mahdi.wang@coreestate.com", Name = "Mahdi Wang", Birthdate = DateTime.Parse("1983-01-10") },
                        new WebUser { UserName = "sadia.porter@coreestate.com", Email = "sadia.porter@coreestate.com", Name = "Sadia Porter", Birthdate = DateTime.Parse("1997-04-07") },
                        new WebUser { UserName = "elisabeth.baker@coreestate.com", Email = "elisabeth.baker@coreestate.com", Name = "Elisabeth Baker", Birthdate = DateTime.Parse("1967-05-12") },
                        new WebUser { UserName = "mason.moody@coreestate.com", Email = "mason.moody@coreestate.com", Name = "Mason Moody", Birthdate = DateTime.Parse("2007-03-13") },
                        new WebUser { UserName = "joshua.kerr@coreestate.com", Email = "joshua.kerr@coreestate.com", Name = "Joshua Kerr", Birthdate = DateTime.Parse("1963-12-12") },
                        new WebUser { UserName = "eliza.john@coreestate.com", Email = "eliza.john@coreestate.com", Name = "Eliza John", Birthdate = DateTime.Parse("1958-03-26") },
                        new WebUser { UserName = "anas.gibson@coreestate.com", Email = "anas.gibson@coreestate.com", Name = "Anas Gibson", Birthdate = DateTime.Parse("1983-02-07") }
                    );
                }

                // Look for any for sale properties.
                if (!context.ForSaleProperties.Any())
                {
                    // Database has not been seeded with for sale properties.
                    context.ForSaleProperties.AddRange(
                        new ForSaleProperty { Name = "", Address = "", Description = "", Price = 0, IsFreehold = true }
                    );
                }

                // Look for any to rent properties.
                if (!context.ToRentProperties.Any())
                {
                    // Database has not been seeded with to rent properties.
                    context.ToRentProperties.AddRange(
                        new ToRentProperty { Name = "", Address = "", Description = "", Rent = 0, Despoit = 0 }
                    );
                }

                context.SaveChanges();

                // Look for any users with IsTestUser role.
                var userManager = serviceProvider.GetRequiredService<UserManager<WebUser>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userExists = await userManager.GetUsersInRoleAsync(RoleName.IsTestUser);

                if (userExists.Count() < 1)
                {
                    // Database has not been seeded with IsTestUser role users.
                    await roleManager.CreateAsync(new IdentityRole(RoleName.IsTestUser));

                    foreach (var user in context.Users)
                    {
                        var res = await userManager.AddToRoleAsync(user, RoleName.IsTestUser);

                        if (res.Succeeded)
                        {
                            await userManager.AddPasswordAsync(user, "Password123#");
                        }
                    }
                }
            }
        }
    }
}
