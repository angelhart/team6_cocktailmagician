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
    public class RateCocktailAsync_Should
    {
        [TestMethod]
        public async Task Throw_When_CocktailNotFound()
        {
            var options = Utility.GetOptions(nameof(Throw_When_CocktailNotFound));

            var mockBarMapper = new Mock<IBarMapper>();
            var mockCocktailMapper = new Mock<ICocktailMapper>();

            var input = new CocktailRatingDTO
            {
                AppUserId = Guid.Parse("b8a3552e-f509-42f0-bed9-7c29c3dfc6b6"), // user1
                CocktailId = Guid.Parse("5416ceee-839d-43e3-bb85-b292976c353e"), // cocktail D
                Score = 3,
            };

            using var assertContext = new CMContext(options);
            {
                var sut = new RatingServices(assertContext, mockCocktailMapper.Object, mockBarMapper.Object);

                await Assert.ThrowsExceptionAsync<NullReferenceException>(() => sut.RateCocktailAsync(input));
            }
        }

        [TestMethod]
        public async Task Throw_When_InputIsNull()
        {
            var options = Utility.GetOptions(nameof(Throw_When_InputIsNull));
            await Utility.ArrangeContextAsync(options);

            var mockBarMapper = new Mock<IBarMapper>();
            var mockCocktailMapper = new Mock<ICocktailMapper>();

            using var assertContext = new CMContext(options);
            {
                var sut = new RatingServices(assertContext, mockCocktailMapper.Object, mockBarMapper.Object);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.RateCocktailAsync(null));
            }
        }

        [TestMethod]
        public async Task ReturnDto_AndUpdateAverage_When_InputValid()
        {
            var options = Utility.GetOptions(nameof(ReturnDto_AndUpdateAverage_When_InputValid));
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

            var arrangeContext = new CMContext(options);

            var input = new CocktailRatingDTO
            {
                AppUserId = Guid.Parse("b8a3552e-f509-42f0-bed9-7c29c3dfc6b6"),
                CocktailId = Guid.Parse("5416ceee-839d-43e3-bb85-b292976c353e"),
                Score = 4,
            };

            var count = await arrangeContext.CocktailRatings
                            .CountAsync(cr => cr.CocktailId == input.CocktailId);

            var sumBefore = await arrangeContext.CocktailRatings
                                    .Where(cr => cr.CocktailId == input.CocktailId)
                                    .SumAsync(cr => cr.Score);

            var expectedAverage = (double)(sumBefore + input.Score) / (count + 1);

            using (var assertContext = new CMContext(options))
            {
                var sut = new RatingServices(assertContext, mockCocktailMapper.Object, mockBarMapper.Object);

                var result = await sut.RateCocktailAsync(input);
                var cocktail = await assertContext.Cocktails.FindAsync(input.CocktailId);
                Assert.AreEqual(input.Score, result.Score);
                Assert.AreEqual(count + 1, cocktail.Ratings.Count());
                Assert.AreEqual(expectedAverage, cocktail.AverageRating);
            }
        }
    }
}
