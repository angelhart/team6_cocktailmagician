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
    public class GetCocktailRatingAsync_Should
    {
        [TestMethod]
        public async Task ReturnDTO_When_CocktailIRatedByUser()
        {
            var options = Utility.GetOptions(nameof(ReturnDTO_When_CocktailIRatedByUser));
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
            var cr = await arrangeContext.CocktailRatings.FirstOrDefaultAsync();

            var input = new CocktailRatingDTO
            {
                AppUserId = cr.AppUserId, // user1
                CocktailId = cr.CocktailId, // cocktail C
            };

            using var assertContext = new CMContext(options);
            {
                var sut = new RatingServices(assertContext, mockCocktailMapper.Object, mockBarMapper.Object);

                var result = await sut.GetCocktailRatingAsync(input.AppUserId, input.CocktailId);
                Assert.AreEqual(cr.Score, result.Score);
            }
        }

        [TestMethod]
        public async Task Throw_When_CocktailNotRatedByUser()
        {
            var options = Utility.GetOptions(nameof(Throw_When_CocktailNotRatedByUser));

            var mockBarMapper = new Mock<IBarMapper>();
            var mockCocktailMapper = new Mock<ICocktailMapper>();

            var input = new CocktailRatingDTO
            {
                AppUserId = Guid.Parse("b8a3552e-f509-42f0-bed9-7c29c3dfc6b6"), // user1
                CocktailId = Guid.Parse("5416ceee-839d-43e3-bb85-b292976c353e"), // cocktail D
            };

            using var assertContext = new CMContext(options);
            {
                var sut = new RatingServices(assertContext, mockCocktailMapper.Object, mockBarMapper.Object);

                await Assert.ThrowsExceptionAsync<NullReferenceException>(() => sut.GetCocktailRatingAsync(input.AppUserId, input.CocktailId));
            }
        }
    }
}
