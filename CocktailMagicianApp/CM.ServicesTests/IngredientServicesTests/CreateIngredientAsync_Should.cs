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
using System.Text;
using System.Threading.Tasks;

namespace CM.ServicesTests.IngredientServicesTests
{
    [TestClass]
    public class CreateIngredientAsync_Should
    {
        [TestMethod]
        public async Task ReturnDTO_When_CreateSuccesfull()
        {
            var options = Utility.GetOptions(nameof(ReturnDTO_When_CreateSuccesfull));
            await Utility.ArrangeContextAsync(options);

            var mockMapper = new Mock<IIngredientMapper>();
            mockMapper.Setup(x => x.CreateIngredient(It.IsAny<IngredientDTO>()))
                      .Returns<IngredientDTO>(i => new Ingredient
                      {
                          Name = i.Name
                      });
            mockMapper.Setup(x => x.CreateIngredientDTO(It.IsAny<Ingredient>()))
                      .Returns<Ingredient>(i => new IngredientDTO
                      {
                          Id = i.Id,
                          Name = i.Name
                      });

            var newDto = new IngredientDTO
            {
                Name = "New Ingredient Name"
            };

            using var assertContext = new CMContext(options);

            var sut = new IngredientServices(assertContext, mockMapper.Object);

            var result = await sut.CreateIngredientAsync(newDto);
            var expected = await assertContext.Ingredients.FindAsync(result.Id);
            //Assert.IsInstanceOfType(result, typeof(IngredientDTO));
            Assert.AreEqual(expected.Name, result.Name);
        }

        [TestMethod]
        public async Task Throw_When_IngredientNameExists()
        {
            var options = Utility.GetOptions(nameof(Throw_When_IngredientNameExists));
            await Utility.ArrangeContextAsync(options);

            var mockMapper = new Mock<IIngredientMapper>();
            mockMapper.Setup(x => x.CreateIngredient(It.IsAny<IngredientDTO>()))
                      .Returns<IngredientDTO>(i => new Ingredient
                      {
                          Name = i.Name
                      });
            mockMapper.Setup(x => x.CreateIngredientDTO(It.IsAny<Ingredient>()))
                      .Returns<Ingredient>(i => new IngredientDTO
                      {
                          Id = i.Id,
                          Name = i.Name
                      });

            var newDto = new IngredientDTO
            {
                Name = "Ingredient A"
            };

            using var assertContext = new CMContext(options);

            var sut = new IngredientServices(assertContext, mockMapper.Object);

            await Assert.ThrowsExceptionAsync<DbUpdateException>(() => sut.CreateIngredientAsync(newDto));
        }
    }
}
