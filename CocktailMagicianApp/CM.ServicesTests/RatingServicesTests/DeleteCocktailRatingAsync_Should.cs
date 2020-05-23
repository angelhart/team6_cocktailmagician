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
    public class DeleteCocktailRatingAsync_Should
    {
        [TestMethod]
        public async Task ReturnDTO_When_InputIsValid()
        {
            var options = Utility.GetOptions(nameof(ReturnDTO_When_InputIsValid));
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
            var countBefore = await arrangeCtx.CocktailRatings.CountAsync();

            var rating = await arrangeCtx.CocktailRatings.FirstAsync();
            var userId = rating.AppUserId;
            var cocktailId = rating.CocktailId;

            using var assertContext = new CMContext(options);
            {
                var sut = new RatingServices(assertContext, mockCocktailMapper.Object, mockBarMapper.Object);

                var result = await sut.DeleteCocktailRatingAsync(userId, cocktailId);
                var countAfter = await assertContext.CocktailRatings.CountAsync();
                Assert.AreEqual(countBefore - 1, countAfter);
            }
        }
    }
}
