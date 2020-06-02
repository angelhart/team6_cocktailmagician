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
                          Recipe = c.Recipe,
                          Ingredients = c.Ingredients
                                         .Select(i => new IngredientDTO
                                         {
                                             Id = i.IngredientId,
                                         })
                                         .ToList(),
                      });

            var updatedDto = new CocktailDTO
            {
                Id = Guid.Parse("e8601248-4de3-4ccb-ab20-563926dedbd5"),  // cocktail B has ingredients A and B
                Name = "New Name", // name change
                Recipe = "New recipe",
                Ingredients = new List<IngredientDTO>
                {
                    // should keep only ingredient A after update
                    new IngredientDTO
                    {
                        Id = Guid.Parse("eb5d7135-f194-4443-a5ff-cc955396648e"), // Ingredient A
                        Ammount = 2,
                        Unit = Unit.ml.ToString(),
                    }
                }
            };

            using var assertContext = new CMContext(options);

            var sut = new CocktailServices(assertContext, mockMapper.Object);
            var result = await sut.UpdateCocktailAsync(updatedDto);
            var entity = await assertContext.Cocktails.FirstOrDefaultAsync(c => c.Name == updatedDto.Name);
            Assert.IsNotNull(entity);
            Assert.AreEqual(entity.Id, result.Id);
            Assert.AreEqual(updatedDto.Recipe, result.Recipe);
            Assert.AreEqual(updatedDto.Ingredients.Count, result.Ingredients.Count);
            Assert.AreEqual(updatedDto.Ingredients.ToList()[0].Id,
                            result.Ingredients.ToList()[0].Id);
        }
    }
}
