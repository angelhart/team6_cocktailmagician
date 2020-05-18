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
    public class UpdateCocktailAsync_Should
    {
        [TestMethod]
        public async Task ReturnDto_When_Updated()
        {
            var options = Utility.GetOptions(nameof(ReturnDto_When_Updated));
            await Utility.ArrangeContextAsync(options);

            var mockMapper = new Mock<ICocktailMapper>();

            mockMapper.Setup(x => x.CreateCocktailDTO(It.IsAny<Cocktail>()))
                      .Returns<Cocktail>(c => new CocktailDTO
                      {
                          Id = c.Id,
                          Name = c.Name,
                          Ingredients = c.Ingredients
                                         .Select(i => new CocktailIngredientDTO
                                         {
                                             IngredientId = i.IngredientId,
                                         })
                                         .ToList(),
                      });

            var updatedDto = new CocktailDTO
            {
                Id = Guid.Parse("e8601248-4de3-4ccb-ab20-563926dedbd5"),  // cocktail B has ingredients A and C
                Name = "New Name", // name change
                Ingredients = new List<CocktailIngredientDTO>
                {
                    // should keep only ingredient A after update
                    new CocktailIngredientDTO
                    {
                        IngredientId = Guid.Parse("eb5d7135-f194-4443-a5ff-cc955396648e") // Ingredient A
                    }
                }
            };

            using var assertContext = new CMContext(options);

            var sut = new CocktailServices(assertContext, mockMapper.Object);
            var result = await sut.UpdateCocktailAsync(updatedDto);
            var expected = await assertContext.Cocktails.FirstOrDefaultAsync(c => c.Name == updatedDto.Name);
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected.Id, result.Id);
            Assert.AreEqual(updatedDto.Ingredients.Count, result.Ingredients.Count);
        }
    }
}
