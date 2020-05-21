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
    public class DeleteIngredientAsync_Should
    {
        [TestMethod]
        public async Task ChangeCollectionSize_WhenIngredientDeleted()
        {
            var options = Utility.GetOptions(nameof(ChangeCollectionSize_WhenIngredientDeleted));
            await Utility.ArrangeContextAsync(options);

            var mockMapper = new Mock<IIngredientMapper>();
            mockMapper.Setup(x => x.CreateIngredientDTO(It.IsAny<Ingredient>()))
                      .Returns<Ingredient>(i => new IngredientDTO
                      {
                          Id = i.Id,
                          Name = i.Name
                      });
            
            // Id of Ingredient C used
            var inputId = Guid.Parse("bce99872-9407-47a3-b3fc-50cb707cb19c");// Guid.Parse("eb5d7135-f194-4443-a5ff-cc955396648e");

            using var assertContext = new CMContext(options);

            var sut = new IngredientServices(assertContext, mockMapper.Object);

            var countBeforeDelete = await assertContext.Ingredients.CountAsync();
            var result = await sut.DeleteIngredientAsync(inputId);
            var countAfterDelete = await assertContext.Ingredients.CountAsync();
            //Assert.IsInstanceOfType(result, typeof(IngredientDTO));
            Assert.AreEqual(countBeforeDelete - 1, countAfterDelete);
        }

        [TestMethod]
        public async Task Throw_When_IngredientIsPartOfCocktail()
        {
            var options = Utility.GetOptions(nameof(Throw_When_IngredientIsPartOfCocktail));
            await Utility.ArrangeContextAsync(options);

            var mockMapper = new Mock<IIngredientMapper>();
            mockMapper.Setup(x => x.CreateIngredientDTO(It.IsAny<Ingredient>()))
                      .Returns<Ingredient>(i => new IngredientDTO
                      {
                          Id = i.Id,
                          Name = i.Name
                      });

            // Id of Ingredient A used
            var inputId = Guid.Parse("eb5d7135-f194-4443-a5ff-cc955396648e");

            using var assertContext = new CMContext(options);

            var sut = new IngredientServices(assertContext, mockMapper.Object);

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => sut.DeleteIngredientAsync(inputId));
        }
    }
}
