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
    public class DeleteCocktailCommentAsync_Should
    {
        [TestMethod]
        public async Task ReturnDto_When_CommentDeleted()
        {
            var options = Utility.GetOptions(nameof(ReturnDto_When_CommentDeleted));
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
            var comment = await arrangeContext.CocktailComments.FirstOrDefaultAsync();
            var commentsCountBefore = await arrangeContext.CocktailComments.CountAsync();

            using (var assertContext = new CMContext(options))
            {
                var sut = new CommentServices(assertContext, mockCocktailMapper.Object, mockBarMapper.Object, mockMapperDateTime.Object);

                var result = await sut.DeleteCocktailCommentAsync(comment.Id);
                var commentsCountAfter = await assertContext.CocktailComments.CountAsync();
                Assert.AreEqual(comment.Id, result.Id);
                Assert.AreEqual(commentsCountBefore - 1, commentsCountAfter);
            }
        }
    }
}
