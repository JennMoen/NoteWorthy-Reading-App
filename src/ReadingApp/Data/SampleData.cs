using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using ReadingApp.Models;

namespace ReadingApp.Data
{
    public class SampleData
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            // Ensure db
            context.Database.EnsureCreated();

            // Ensure Stephen (IsAdmin)
            var stephen = await userManager.FindByNameAsync("Stephen.Walther@CoderCamps.com");
            if (stephen == null)
            {
                // create user
                stephen = new ApplicationUser
                {
                    UserName = "Stephen.Walther@CoderCamps.com",
                    Email = "Stephen.Walther@CoderCamps.com"
                };
                await userManager.CreateAsync(stephen, "Secret123!");

                // add claims
                await userManager.AddClaimAsync(stephen, new Claim("IsAdmin", "true"));
            }

            // Ensure Mike (not IsAdmin)
            var mike = await userManager.FindByNameAsync("Mike@CoderCamps.com");
            if (mike == null)
            {
                // create user
                mike = new ApplicationUser
                {
                    UserName = "Mike@CoderCamps.com",
                    Email = "Mike@CoderCamps.com"
                };
                await userManager.CreateAsync(mike, "Secret123!");
            }

            if (!context.Resources.Any())
            {
                context.Resources.AddRange(
                    new Resource()
                    {

                        Title = "Wuthering Heights",
                        Author = "Emily Bronte",
                        ImageUrl = "http://livethroughbooks.files.wordpress.com/2010/04/wuthering-heights.jpg",
                        Link = "https://books.google.com/books?id=0isRIr1kzaQC&printsec=frontcover&dq=wuthering+heights&hl=en&sa=X&ved=0ahUKEwj77vK7ho_OAhXTdSYKHdx_D9AQ6AEIHjAA#v=onepage&q=wuthering%20heights&f=false",
                        DateCreated = new DateTime(2016, 07, 25),
                        LastUpdated = new DateTime(2016, 07, 25),
                        User = mike

                    },

                    new Resource()
                    {

                        Title = "Fight Club",
                        Author = "Chuck Palahniuk",
                        ImageUrl = "http://d.gr-assets.com/books/1357128997l/5759.jpg",
                        Link = "http://www.goodreads.com/book/show/5759.Fight_Club",
                        DateCreated = new DateTime(2015, 07, 28),
                        LastUpdated = new DateTime(2015, 07, 30),
                        User = mike



                    }

                    );

                context.SaveChanges();

            }

            if (!context.Comments.Any())
            {
                context.Comments.AddRange(
                    new Comment()
                    {
                        Location = "p.35",
                        Text = "Foreshadowing of the tragic ending",
                        ResourceId = 1
                    }

                    );
                context.SaveChanges();

            }
        }
    }

}
