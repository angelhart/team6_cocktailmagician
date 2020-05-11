using CM.Data;
using CM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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

        public static void ContextWithCocktails(DbContextOptions options)
        {
            var cocktails = new List<Cocktail> {
                    new Cocktail
                {
                    Id = Guid.Parse("9b9f85e3-51be-4fbf-918a-9fbd89546ef7"),
                    Name = "Name A"
                },
                    new Cocktail
                {
                    Id = Guid.Parse("e8601248-4de3-4ccb-ab20-563926dedbd5"),
                    Name = "Name C"
                },
                    new Cocktail
                {
                    Id = Guid.Parse("9344e67f-f9a9-45c3-b583-7378387bf862"),
                    Name = "Name B"
                },
            };
        }
    }
}
