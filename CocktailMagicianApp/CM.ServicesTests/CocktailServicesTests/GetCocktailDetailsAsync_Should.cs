using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers.Contracts;
using CM.Models;
using CM.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.ServicesTests.CocktailServicesTests
{
    [TestClass]
    public class GetCocktailDetailsAsync_Should
    {
        [TestMethod]
        public async Task ReturnDto_When_ListedFound()
        {
            var options = Utility.GetOptions(nameof(ReturnDto_When_ListedFound));
            await Utility.ArrangeContextAsync(options);

            var mockMapper = new Mock<ICocktailMapper>();
            mockMapper.Setup(x => x.CreateCocktailDTO(It.IsAny<Cocktail>()))
                      .Returns<Cocktail>(c => new CocktailDTO
                      {
                          Id = c.Id,
                          AverageRating = c.AverageRating
                      });

            var existingId = Guid.Parse("9b9f85e3-51be-4fbf-918a-9fbd89546ef7"); // Cocktail A

            using var assertContext = new CMContext(options);

            var sut = new CocktailServices(assertContext, mockMapper.Object);
            var result = await sut.GetCocktailDetailsAsync(existingId);
            var expected = await assertContext.Cocktails.FindAsync(existingId);
            Assert.AreEqual(expected.Id, result.Id);
            Assert.AreEqual(expected.AverageRating, result.AverageRating);

        }

        [TestMethod]
        public async Task Throw_When_UnlistedNotAllowed()
        {
            var options = Utility.GetOptions(nameof(Throw_When_UnlistedNotAllowed));
            await Utility.ArrangeContextAsync(options);

            var mockMapper = new Mock<ICocktailMapper>();
            mockMapper.Setup(x => x.CreateCocktailDTO(It.IsAny<Cocktail>()))
                      .Returns<Cocktail>(c => new CocktailDTO
                      {
                          Id = c.Id,
                          AverageRating = c.AverageRating
                      });

            var unlistedId = Guid.Parse("9344e67f-f9a9-45c3-b583-7378387bf862"); // Cocktail C

            using var assertContext = new CMContext(options);

            var sut = new CocktailServices(assertContext, mockMapper.Object);
            await Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(() => sut.GetCocktailDetailsAsync(unlistedId));
        }

        [TestMethod]
        public async Task ReturnDto_When_UnlistedAllowed()
        {
            var options = Utility.GetOptions(nameof(ReturnDto_When_UnlistedAllowed));
            await Utility.ArrangeContextAsync(options);

            var mockMapper = new Mock<ICocktailMapper>();
            mockMapper.Setup(x => x.CreateCocktailDTO(It.IsAny<Cocktail>()))
                      .Returns<Cocktail>(c => new CocktailDTO
                      {
                          Id = c.Id,
                          AverageRating = c.AverageRating
                      });

            var unlistedId = Guid.Parse("9344e67f-f9a9-45c3-b583-7378387bf862"); // Cocktail C

            using var assertContext = new CMContext(options);

            var sut = new CocktailServices(assertContext, mockMapper.Object);
            var result = await sut.GetCocktailDetailsAsync(unlistedId, true);
            var expected = await assertContext.Cocktails.FindAsync(unlistedId);
            Assert.AreEqual(expected.Id, result.Id);
            Assert.AreEqual(expected.AverageRating, result.AverageRating);
        }

        [TestMethod]
        public async Task Throw_When_KeyNotFound()
        {
            var options = Utility.GetOptions(nameof(Throw_When_KeyNotFound));
            await Utility.ArrangeContextAsync(options);

            var mockMapper = new Mock<ICocktailMapper>();
            mockMapper.Setup(x => x.CreateCocktailDTO(It.IsAny<Cocktail>()))
                      .Returns<Cocktail>(c => new CocktailDTO
                      {
                          Id = c.Id,
                          AverageRating = c.AverageRating
                      });

            var unlistedId = Guid.Parse("00000000-0000-0000-0000-000000000000");

            using var assertContext = new CMContext(options);

            var sut = new CocktailServices(assertContext, mockMapper.Object);
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => sut.GetCocktailDetailsAsync(unlistedId));
        }
    }
}
