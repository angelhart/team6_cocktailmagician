using Castle.DynamicProxy.Generators;
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

namespace CM.ServicesTests.CommentServicesTests
{
    [TestClass]
    public class EditCocktailCommentAsync_Should
    {
        [TestMethod]
        public async Task ReturnDto_When_CommentEdited()
        {
            var options = Utility.GetOptions(nameof(ReturnDto_When_CommentEdited));
            await Utility.ArrangeContextAsync(options);

            var mockBarMapper = new Mock<IBarMapper>();

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

            var input = "Updated lorem ipsum";

            using (var assertContext = new CMContext(options))
            {
                var sut = new CommentServices(assertContext, mockCocktailMapper.Object, mockBarMapper.Object);

                var result = await sut.EditCocktailCommentAsync(comment.Id, input);
                Assert.AreEqual(comment.Id, result.Id);
                Assert.AreEqual(input, result.Text);
            }
        }
    }
}
