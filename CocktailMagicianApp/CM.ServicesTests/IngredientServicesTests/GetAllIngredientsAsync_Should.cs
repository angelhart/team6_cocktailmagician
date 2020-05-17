using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers.Contracts;
using CM.Models;
using CM.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace CM.ServicesTests.IngredientServicesTests
{
    [TestClass]
    public class GetAllIngredientsAsync_Should
    {
        [TestMethod]
        public async Task ReturnCollection_WhenAnyPresent()
        {
            var options = Utility.GetOptions(nameof(ReturnCollection_WhenAnyPresent));
            await Utility.ArrangeContextAsync(options);

            var mockMapper = new Mock<IIngredientMapper>();
            mockMapper.Setup(x => x.CreateIngredientDTO(It.IsAny<Ingredient>()))
                      .Returns<Ingredient>(i => new IngredientDTO
                      {
                          Id = i.Id,
                          Name = i.Name
                      });

            using var assertContext = new CMContext(options);

            var sut = new IngredientServices(assertContext, mockMapper.Object);

            var result = await sut.GetAllIngredientsAsync();
            var expected = await assertContext.Ingredients.ToListAsync();

            Assert.AreNotEqual(0, expected.Count);
            Assert.AreEqual(expected.Count, result.Count);
        }

        [TestMethod]
        public async Task ReturnEmptyCollection_WhenNonPresent()
        {
            var options = Utility.GetOptions(nameof(ReturnEmptyCollection_WhenNonPresent));

            var mockMapper = new Mock<IIngredientMapper>();
            mockMapper.Setup(x => x.CreateIngredientDTO(It.IsAny<Ingredient>()))
                      .Returns<Ingredient>(i => new IngredientDTO
                      {
                          Id = i.Id,
                          Name = i.Name
                      });

            using var assertContext = new CMContext(options);

            var sut = new IngredientServices(assertContext, mockMapper.Object);

            var result = await sut.GetAllIngredientsAsync();
            var expected = await assertContext.Ingredients.ToListAsync();
            Assert.AreEqual(0, expected.Count);
            Assert.AreEqual(expected.Count, result.Count);
        }
    }
}
