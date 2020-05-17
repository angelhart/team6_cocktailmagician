using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers.Contracts;
using CM.Models;
using CM.Services;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.ServicesTests.IngredientServicesTests
{
    // Since method is private, tests carried out via GetIngredientDetailsAsync(Guid id)

    [TestClass]
    public class GetIngredientDetailsAsync_Should
    {
        [TestMethod]
        public async Task ReturnDto_WhenIdFound()
        {
            var options = Utility.GetOptions(nameof(ReturnDto_WhenIdFound));
            await Utility.ArrangeContextAsync(options);

            var mockMapper = new Mock<IIngredientMapper>();
            mockMapper.Setup(x => x.CreateIngredientDTO(It.IsAny<Ingredient>()))
                      .Returns<Ingredient>(i => new IngredientDTO 
                      {
                          Id = i.Id,
                          Name = i.Name
                      });

            var lookUpId = Guid.Parse("eb5d7135-f194-4443-a5ff-cc955396648e");

            using var assertContext = new CMContext(options);

            var sut = new IngredientServices(assertContext, mockMapper.Object);

            var result = await sut.GetIngredientDetailsAsync(lookUpId);
            var expected = await assertContext.Ingredients.FindAsync(lookUpId);
            Assert.AreEqual(expected.Name, result.Name);
        }

        [TestMethod]
        public async Task Throw_WhenId_NotFound()
        {
            var options = Utility.GetOptions(nameof(Throw_WhenId_NotFound));

            var mockMapper = new Mock<IIngredientMapper>();
            
            var nonExistingId = Guid.Parse("00000000-0000-0000-0000-000000000000");

            using var assertContext = new CMContext(options);

            var sut = new IngredientServices(assertContext, mockMapper.Object);

            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => sut.GetIngredientDetailsAsync(nonExistingId));
        }
    }
}
