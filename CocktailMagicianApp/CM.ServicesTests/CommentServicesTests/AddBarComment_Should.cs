using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers.Contracts;
using CM.Models;
using CM.Services;
using CM.Services.Providers.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CM.ServicesTests.CommentServicesTests
{
	[TestClass]
	public class AddBarComment_Should
	{
        [TestMethod]
        public async Task ThrowArgumentNullException_When_InputNull()
        {
            var options = Utility.GetOptions(nameof(ThrowArgumentNullException_When_InputNull));

            var mockBarMapper = new Mock<IBarMapper>();
            var mockMapperDateTime = new Mock<IDateTimeProvider>();

            var mockCocktailMapper = new Mock<ICocktailMapper>();

            using (var assertContext = new CMContext(options))
            {
                var sut = new CommentServices(assertContext, mockCocktailMapper.Object, mockBarMapper.Object, mockMapperDateTime.Object);
                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.AddBarCommentAsync(null));
            }
        }

        [TestMethod]
        public async Task CommentCreated_ValidInput()
        {
            var options = Utility.GetOptions(nameof(CommentCreated_ValidInput));
            await Utility.ArrangeContextAsync(options);

            var mockBarMapper = new Mock<IBarMapper>();
            var mockMapperDateTime = new Mock<IDateTimeProvider>();
            var mockCocktailMapper = new Mock<ICocktailMapper>();

            var arrangeContext = new CMContext(options);
            var user = await arrangeContext.Users.FirstOrDefaultAsync();
            var bar = await arrangeContext.Bars
                                    .Include(c => c.Comments)
                                    .FirstOrDefaultAsync();
            var time = DateTimeOffset.UtcNow;

            var input = new BarCommentDTO
            {
                BarId = bar.Id,
                UserId = user.Id,
                Text = "Test text",
                CommentedOn = time,
            };

            var commentsBeforeAdd = bar.Comments.Count;

            using (var assertContext = new CMContext(options))
            {
                var sut = new CommentServices(assertContext, mockCocktailMapper.Object, mockBarMapper.Object, mockMapperDateTime.Object);

               await sut.AddBarCommentAsync(input);
               var updatedBar = await arrangeContext.Bars
                                        .Include(b => b.Comments)
                                        .FirstOrDefaultAsync(b => b.Id == bar.Id);
                Assert.AreEqual(commentsBeforeAdd + 1, updatedBar.Comments.Count);
            }
        }
    }
}
