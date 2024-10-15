using GuiderPro.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiderPro.Persistence
{
    public static class DbInitializer
    {
        public static async Task Initialize(AppDbContext context)
        {
            await context.Database.MigrateAsync();

            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Restaurant", Description = "Places to eat" },
                    new Category { Name = "Cafe", Description = "Coffee and snacks" },
                    new Category { Name = "Bar", Description = "Nightlife spots" },
                    new Category { Name = "Gym", Description = "Fitness centers" },
                    new Category { Name = "Spa", Description = "Relaxation and wellness" },
                    new Category { Name = "Supermarket", Description = "Food and groceries" },
                    new Category { Name = "Pharmacy", Description = "Medical supplies" },
                    new Category { Name = "Cinema", Description = "Entertainment and movies" },
                    new Category { Name = "Park", Description = "Outdoor relaxation" },
                    new Category { Name = "Bookstore", Description = "Books and stationery" }
                };

                await context.Categories.AddRangeAsync(categories);

                await context.SaveChangesAsync();
            }

            if (!context.Tags.Any())
            {
                var tags = new List<Tag>
                {
                    new Tag { Name = "Vegan", Description = "Vegan-friendly places" },
                    new Tag { Name = "24/7", Description = "Open 24/7" },
                    new Tag { Name = "Family", Description = "Family-friendly" },
                    new Tag { Name = "Pet Friendly", Description = "Places that allow pets" },
                    new Tag { Name = "WiFi", Description = "Free Wi-Fi available" },
                    new Tag { Name = "Live Music", Description = "Live performances" },
                    new Tag { Name = "Outdoor Seating", Description = "Places with outdoor seating" },
                    new Tag { Name = "Smoking Allowed", Description = "Smoking-friendly areas" },
                    new Tag { Name = "Wheelchair Accessible", Description = "Accessible for people with disabilities" },
                    new Tag { Name = "Delivery", Description = "Delivery services available" }
                };

                await context.Tags.AddRangeAsync(tags);

                await context.SaveChangesAsync();
            }
        }
    }
}
