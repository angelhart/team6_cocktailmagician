using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers.Contracts;
using CM.Models;
using CM.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
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
    public class CreateCocktailAsync_Should
    {
        [TestMethod]
        public async Task ReturnDto_When_Created()
        {
            var options = Utility.GetOptions(nameof(ReturnDto_When_Created));
            await Utility.ArrangeContextAsync(options);

            var mockMapper = new Mock<ICocktailMapper>();

            mockMapper.Setup(x => x.CreateCocktailDTO(It.IsAny<Cocktail>()))
                      .Returns<Cocktail>(c => new CocktailDTO
                      {
                          Id = c.Id,
                          Name = c.Name,
                          Ingredients = c.Ingredients
                                         .Select(i => new IngredientDTO
                                         {
                                             Id = i.IngredientId,
                                         })
                                         .ToList(),
                      });

            var input = new CocktailDTO
            {
                Name = "New Name",
                Recipe = "Recipe text",
                Ingredients = new List<IngredientDTO>
                {
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
            var result = await sut.CreateCocktailAsync(input);
            var expected = await assertContext.Cocktails.FirstOrDefaultAsync(c => c.Name == input.Name);
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected.Id, result.Id);
            Assert.AreEqual(input.Ingredients.Count, result.Ingredients.Count);
        }

        [TestMethod]
        public async Task Throw_When_NameExists()
        {
            var options = Utility.GetOptions(nameof(Throw_When_NameExists));
            await Utility.ArrangeContextAsync(options);

            var mockMapper = new Mock<ICocktailMapper>();

            mockMapper.Setup(x => x.CreateCocktailDTO(It.IsAny<Cocktail>()))
                      .Returns<Cocktail>(c => new CocktailDTO
                      {
                          Id = c.Id,
                          Name = c.Name
                      });

            var input = new CocktailDTO
            {
                Name = "Cocktail A"
            };

            using var assertContext = new CMContext(options);

            var sut = new CocktailServices(assertContext, mockMapper.Object);
            await Assert.ThrowsExceptionAsync<DbUpdateException>(() => sut.CreateCocktailAsync(input));
        }
    }
}
