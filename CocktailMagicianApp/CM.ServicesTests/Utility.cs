using CM.Data;
using CM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CM.ServicesTests
{
    public static class Utility
    {
        public static DbContextOptions<CMContext> GetOptions(string tempDbName)
        {
            return new DbContextOptionsBuilder<CMContext>()
                .UseInMemoryDatabase(tempDbName)
                .Options;
        }

        public static async Task ArrangeContextAsync(DbContextOptions options)
        {
            var user1 = new AppUser
            {
                Id = Guid.Parse("b8a3552e-f509-42f0-bed9-7c29c3dfc6b6")
            };

            var user2 = new AppUser
            {
                Id = Guid.Parse("b54a920d-0766-4532-8dd8-d98b1df79b37")
            };

            var cocktails = new List<Cocktail>
            {
                new Cocktail
                {
                    Id = Guid.Parse("9b9f85e3-51be-4fbf-918a-9fbd89546ef7"),
                    Name = "Cocktail A",
                    Ratings = new List<CocktailRating>
                    {
                        new CocktailRating
                        {
                             AppUser = user1,
                             Score = 5
                        },
                        new CocktailRating
                        {
                             AppUser = user2,
                             Score = 4
                        }
                    },
                    Ingredients = new List<CocktailIngredient>
                    {
                        new CocktailIngredient
                        {
                            IngredientId = Guid.Parse("eb5d7135-f194-4443-a5ff-cc955396648e") // ingredient A
                        }
                    },
                    AverageRating = 9 / 2d
                },
                new Cocktail
                {
                    Id = Guid.Parse("e8601248-4de3-4ccb-ab20-563926dedbd5"),
                    Name = "Cocktail B",
                    Ratings = new List<CocktailRating>
                    {
                        new CocktailRating
                        {
                             AppUser = user1,
                             Score = 2
                        },
                        new CocktailRating
                        {
                             AppUser = user2,
                             Score = 1
                        }
                    },
                    Ingredients = new List<CocktailIngredient>
                    {
                        new CocktailIngredient
                        {
                            IngredientId = Guid.Parse("eb5d7135-f194-4443-a5ff-cc955396648e") // ingredient A
                        },
                        new CocktailIngredient
                        {
                            IngredientId = Guid.Parse("bce99872-9407-47a3-b3fc-50cb707cb19c") // ingredient C
                        }
                    },
                    AverageRating = 3 / 2d
                },
                new Cocktail // unlisted
                {
                    Id = Guid.Parse("9344e67f-f9a9-45c3-b583-7378387bf862"),
                    Name = "Cocktail C", // unlisted
                    Ratings = new List<CocktailRating>
                    {
                        new CocktailRating
                        {
                             AppUser = user1,
                             Score = 3
                        },
                        new CocktailRating
                        {
                             AppUser = user2,
                             Score = 2
                        }
                    },
                    Ingredients = new List<CocktailIngredient>
                    {
                        new CocktailIngredient
                        {
                            IngredientId = Guid.Parse("966528b9-0ab8-4330-8974-b1bb9709ae74") // ingredient B
                        }
                    },
                    IsUnlisted = true // unlisted
                },
            };

            var ingredients = new List<Ingredient>
            {
                new Ingredient
                {
                    Id = Guid.Parse("eb5d7135-f194-4443-a5ff-cc955396648e"),
                    Name = "Ingredient A"
                },
                new Ingredient
                {
                    Id = Guid.Parse("966528b9-0ab8-4330-8974-b1bb9709ae74"),
                    Name = "Ingredient B"
                },
                new Ingredient
                {
                    // This one intentionally not used in any cocktail
                    Id = Guid.Parse("bce99872-9407-47a3-b3fc-50cb707cb19c"),
                    Name = "Ingredient C"
                },
            };

            var cocktailIngredients = new List<CocktailIngredient>
            {
                new CocktailIngredient
                {
                    // Cocktail A has ingredient A
                    CocktailId = Guid.Parse("9b9f85e3-51be-4fbf-918a-9fbd89546ef7"),
                    IngredientId = Guid.Parse("eb5d7135-f194-4443-a5ff-cc955396648e")
                },
                new CocktailIngredient
                {
                    // Cocktail B has ingredients A
                    CocktailId = Guid.Parse("e8601248-4de3-4ccb-ab20-563926dedbd5"),
                    IngredientId = Guid.Parse("eb5d7135-f194-4443-a5ff-cc955396648e")
                },
                new CocktailIngredient
                {
                    // Cocktail B has ingredient B
                    CocktailId = Guid.Parse("e8601248-4de3-4ccb-ab20-563926dedbd5"),
                    IngredientId = Guid.Parse("966528b9-0ab8-4330-8974-b1bb9709ae74")
                },
                // Cocktail C has no ingredients
            };

            using var arrangeContext = new CMContext(options);

            await arrangeContext.AddAsync(user1);
            await arrangeContext.AddAsync(user2);
            await arrangeContext.AddRangeAsync(cocktails);
            await arrangeContext.AddRangeAsync(ingredients);
            await arrangeContext.AddRangeAsync(cocktailIngredients);
            await arrangeContext.SaveChangesAsync();
        }
    }
}
