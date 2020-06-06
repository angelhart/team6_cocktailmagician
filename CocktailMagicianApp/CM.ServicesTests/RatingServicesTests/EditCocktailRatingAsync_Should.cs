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

namespace CM.ServicesTests.RatingServicesTests
{
    [TestClass]
    public class EditCocktailRatingAsync_Should
    {
        [TestMethod]
        public async Task Throw_When_NullInput()
        {
            var options = Utility.GetOptions(nameof(Throw_When_NullInput));

            var mockBarMapper = new Mock<IBarMapper>();
            var mockCocktailMapper = new Mock<ICocktailMapper>();

            using var assertContext = new CMContext(options);
            {
                var sut = new RatingServices(assertContext, mockCocktailMapper.Object, mockBarMapper.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.EditCocktailRatingAsync(null));
            }
        }

        [TestMethod]
        public async Task ReturnDRO_When_InputIsValid()
        {
            var options = Utility.GetOptions(nameof(ReturnDRO_When_InputIsValid));
            await Utility.ArrangeContextAsync(options);

            var mockBarMapper = new Mock<IBarMapper>();

            var mockCocktailMapper = new Mock<ICocktailMapper>();
            mockCocktailMapper.Setup(x => x.CreateCocktailRatingDTO(It.IsAny<CocktailRating>()))
                              .Returns<CocktailRating>(cr => new CocktailRatingDTO
                              {
                                  AppUserId = cr.AppUserId,
                                  CocktailId = cr.CocktailId,
                                  Score = cr.Score
                              });

            var arrangeCtx = new CMContext(options);
            var rating = await arrangeCtx.CocktailRatings.FirstAsync();
            var input = new CocktailRatingDTO
            {
                AppUserId = rating.AppUserId,
                CocktailId = rating.CocktailId,
                Score = new Random().Next(1,6),
            };

            using var assertContext = new CMContext(options);
            {
                var sut = new RatingServices(assertContext, mockCocktailMapper.Object, mockBarMapper.Object);

                var result = await sut.EditCocktailRatingAsync(input);
                Assert.AreEqual(input.Score, result.Score);
            }
        }
    }
}
