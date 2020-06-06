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
    public class GetCocktailCommentAsync_Should
    {
        [TestMethod]
        public async Task Throw_When_CommentNotFound()
        {
            var options = Utility.GetOptions(nameof(Throw_When_CommentNotFound));

            var mockBarMapper = new Mock<IBarMapper>();
            var mockMapperDateTime = new Mock<IDateTimeProvider>();


            var mockCocktailMapper = new Mock<ICocktailMapper>();

            using (var assertContext = new CMContext(options))
            {
                var sut = new CommentServices(assertContext, mockCocktailMapper.Object, mockBarMapper.Object, mockMapperDateTime.Object);
                await Assert.ThrowsExceptionAsync<NullReferenceException>(() => sut.GetCocktailCommentAsync(Guid.Empty));
            }
        }

        [TestMethod]
        public async Task ReturnDto_When_CommentFound()
        {
            var options = Utility.GetOptions(nameof(ReturnDto_When_CommentFound));
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

            using (var assertContext = new CMContext(options))
            {
                var sut = new CommentServices(assertContext, mockCocktailMapper.Object, mockBarMapper.Object, mockMapperDateTime.Object);

                var result = await sut.GetCocktailCommentAsync(comment.Id);
                Assert.AreEqual(comment.Id, result.Id);
                Assert.AreEqual(comment.Text, result.Text);
            }
        }
    }
}
