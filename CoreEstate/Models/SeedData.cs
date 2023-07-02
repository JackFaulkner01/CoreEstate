using CoreEstate.Data;
using Microsoft.EntityFrameworkCore;

namespace CoreEstate.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context == null
                    || context.Customer == null)
                {
                    throw new ArgumentNullException("Null ApplicationDbContext.");
                }

                // Look for customers.
                if (!context.Customer.Any())
                {
                    // Database has not been seeded for customers.
                    context.Customer.AddRange(
                        new Customer { Name = "Husna Johns", Birthdate = DateTime.Parse("1975-09-24") },
                        new Customer { Name = "Dennis Hall", Birthdate = DateTime.Parse("1968-12-01") },
                        new Customer { Name = "Ebony Obrien", Birthdate = DateTime.Parse("1958-11-11") },
                        new Customer { Name = "Hugo Dalton", Birthdate = DateTime.Parse("1983-11-19") },
                        new Customer { Name = "Lochlan Bruce", Birthdate = DateTime.Parse("1976-10-19") },
                        new Customer { Name = "Gloria Flores", Birthdate = DateTime.Parse("1982-11-19") },
                        new Customer { Name = "Arron Haley", Birthdate = DateTime.Parse("1951-07-06") },
                        new Customer { Name = "Kamil Peterson", Birthdate = DateTime.Parse("1962-03-22") },
                        new Customer { Name = "Jaxon Leon", Birthdate = DateTime.Parse("1969-10-15") },
                        new Customer { Name = "Rita Bush", Birthdate = DateTime.Parse("1982-01-23") },
                        new Customer { Name = "Livia Horne", Birthdate = DateTime.Parse("1994-11-15") },
                        new Customer { Name = "Zaki Mckinney", Birthdate = DateTime.Parse("1959-02-07") },
                        new Customer { Name = "Gail Carey", Birthdate = DateTime.Parse("1961-04-27") },
                        new Customer { Name = "Mahdi Wang", Birthdate = DateTime.Parse("1983-01-10") },
                        new Customer { Name = "Sadia Porter", Birthdate = DateTime.Parse("1997-04-07") },
                        new Customer { Name = "Elisabeth Baker", Birthdate = DateTime.Parse("1967-05-12") },
                        new Customer { Name = "Mason Moody", Birthdate = DateTime.Parse("2007-03-13") },
                        new Customer { Name = "Joshua Kerr", Birthdate = DateTime.Parse("1963-12-12") },
                        new Customer { Name = "Eliza John", Birthdate = DateTime.Parse("1958-03-26") },
                        new Customer { Name = "Anas Gibson", Birthdate = DateTime.Parse("1983-02-07") }
                    );
                }

                context.SaveChanges();
            }
        }
    }
}
