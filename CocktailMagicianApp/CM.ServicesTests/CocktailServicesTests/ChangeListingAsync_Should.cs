using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers.Contracts;
using CM.Models;
using CM.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.ServicesTests.CocktailServicesTests
{
    [TestClass]
    public class ChangeListingAsync_Should
    {
        [TestMethod]
        public async Task ReturnDto_When_ListingUpdated()
        {
            var options = Utility.GetOptions(nameof(ReturnDto_When_ListingUpdated));
            await Utility.ArrangeContextAsync(options);

            var mockMapper = new Mock<ICocktailMapper>();

            mockMapper.Setup(x => x.CreateCocktailDTO(It.IsAny<Cocktail>()))
                      .Returns<Cocktail>(c => new CocktailDTO
                      {
                          Id = c.Id,
                          Name = c.Name,
                          Recipe = c.Recipe,
                          IsUnlisted = c.IsUnlisted,
                          Ingredients = c.Ingredients
                                         .Select(i => new CocktailIngredientDTO
                                         {
                                             IngredientId = i.IngredientId,
                                         })
                                         .ToList(),
                      });

            var cocktailId = Guid.Parse("9344e67f-f9a9-45c3-b583-7378387bf862"); // unlisted in InMemory
            var newState = false;

            using var assertContext = new CMContext(options);
            {
                var sut = new CocktailServices(assertContext, mockMapper.Object);
                var result = await sut.ChangeListingAsync(cocktailId, newState);
                var expected = await assertContext.Cocktails.FindAsync(cocktailId);
                Assert.AreEqual(expected.IsUnlisted, result.IsUnlisted);
            }
        }
    }
}
