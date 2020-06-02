using Castle.DynamicProxy.Generators;
using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers.Contracts;
using CM.Models;
using CM.Services;
using CM.Services.Providers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.ServicesTests.CommentServicesTests
{
    [TestClass]
    public class AddCocktailCommentAsync_Should
    {
        [TestMethod]
        public async Task Throw_When_InputNull()
        {
            var options = Utility.GetOptions(nameof(Throw_When_InputNull));

            var mockBarMapper = new Mock<IBarMapper>();
            var mockMapperDateTime = new Mock<IDateTimeProvider>();

            var mockCocktailMapper = new Mock<ICocktailMapper>();

            using (var assertContext = new CMContext(options))
            {
                var sut = new CommentServices(assertContext, mockCocktailMapper.Object, mockBarMapper.Object, mockMapperDateTime.Object);
                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.AddCocktailCommentAsync(null));
            }
        }

        [TestMethod]
        public async Task ReturnDto_When_CommentCreated()
        {
            var options = Utility.GetOptions(nameof(ReturnDto_When_CommentCreated));
            await Utility.ArrangeContextAsync(options);

            var mockBarMapper = new Mock<IBarMapper>();
            var mockMapperDateTime = new Mock<IDateTimeProvider>();


            var mockCocktailMapper = new Mock<ICocktailMapper>();
            mockCocktailMapper.Setup(x => x.CreateCocktailCommentDTO(It.IsAny<CocktailComment>()))
                              .Returns<CocktailComment>(cc => new CocktailCommentDTO
                              {
                                  Id = cc.Id,
                                  CocktailId = cc.CocktailId,
                                  UserId = cc.AppUserId,
                                  Text = cc.Text
                              });

            var arrangeContext = new CMContext(options);
            var user = await arrangeContext.Users.FirstOrDefaultAsync();
            var cocktail = await arrangeContext.Cocktails
                                    .Include(c => c.Comments)
                                    .FirstOrDefaultAsync();
            var time = DateTimeOffset.UtcNow;

            var input = new CocktailCommentDTO
            {
                CocktailId = cocktail.Id,
                UserId = user.Id,
                Text = "Test text",
                CommentedOn = time,
            };

            var commentsBeforeAdd = cocktail.Comments.Count;

            using (var assertContext = new CMContext(options))
            {
                var sut = new CommentServices(assertContext, mockCocktailMapper.Object, mockBarMapper.Object, mockMapperDateTime.Object);

                var result = await sut.AddCocktailCommentAsync(input);
                var updatedCocktail = await arrangeContext.Cocktails
                                        .Include(c => c.Comments)
                                        .FirstOrDefaultAsync(c => c.Id == cocktail.Id);
                Assert.AreEqual(commentsBeforeAdd + 1, updatedCocktail.Comments.Count);
                Assert.AreEqual(input.Text, result.Text);
            }
        }
    }
}
