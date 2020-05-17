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
    public class UpdateIngredientAsync_Should
    {
        [TestMethod]
        public async Task ReturnDTO_WithUpdatedProperties()
        {
            var options = Utility.GetOptions(nameof(ReturnDTO_WithUpdatedProperties));
            await Utility.ArrangeContextAsync(options);

            var mockMapper = new Mock<IIngredientMapper>();
            mockMapper.Setup(x => x.CreateIngredientDTO(It.IsAny<Ingredient>()))
                      .Returns<Ingredient>(i => new IngredientDTO
                      {
                          Id = i.Id,
                          Name = i.Name
                      });

            var updateDto = new IngredientDTO
            {
                // Id of Ingredient A
                Id = Guid.Parse("eb5d7135-f194-4443-a5ff-cc955396648e"),
                Name = "New Ingredient Name"
            };

            using var assertContext = new CMContext(options);

            var sut = new IngredientServices(assertContext, mockMapper.Object);

            var result = await sut.UpdateIngredientAsync(updateDto);
            var expected = await assertContext.Ingredients.FindAsync(result.Id);
            //Assert.IsInstanceOfType(result, typeof(IngredientDTO));
            Assert.AreEqual(expected.Name, result.Name);
        }
    }
}
