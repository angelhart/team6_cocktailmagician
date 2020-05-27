using System;
using System.Linq;
using System.Threading.Tasks;
using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers.Contracts;
using CM.Models;
using CM.Services;
using CM.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CM.ServicesTests.BarServicesTests
{
	[TestClass]
	public class GetTopBarsAsync_Should
	{
		[TestMethod]
		public async Task ReturnCorrectBarsByAvaregeRating()
		{
			//Arrange
			var options = Utility.GetOptions(nameof(ReturnCorrectBarsByAvaregeRating));
			await Utility.ArrangeContextAsync(options);

			var mockMapper = new Mock<IBarMapper>();
			mockMapper.Setup(x => x.CreateBarDTO(It.IsAny<Bar>()))
					  .Returns<Bar>(bar => new BarDTO
					  {
						  Id = bar.Id,
						  Name = bar.Name,
						  AverageRating = bar.AverageRating
					  });

			var mockMapperAddress = new Mock<IAddressServices>();
			mockMapperAddress.Setup(x => x.CreateAddressAsync(It.IsAny<AddressDTO>()));

			//Ratings for Bar Dante - AvgRating 3
			var barRating1 = new BarRating
			{
				BarId = Guid.Parse("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd"),
				AppUserId = Guid.Parse("9bdbf5e7-ad83-415c-b359-9ff5e2f0ded1"),
				Score = 2
			};
			var barRating2 = new BarRating
			{
				BarId = Guid.Parse("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd"),
				AppUserId = Guid.Parse("9bdbf5e7-ad83-415c-b359-9ff5e2f0ded2"),
				Score = 2
			};
			var barRating3 = new BarRating
			{
				BarId = Guid.Parse("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd"),
				AppUserId = Guid.Parse("9bdbf5e7-ad83-415c-b359-9ff5e2f0ded3"),
				Score = 5
			};

			//Ratings for Connaught Bar - AvgRating 2
			var barRating4 = new BarRating
			{
				BarId = Guid.Parse("0899e918-977c-44d5-a5cb-de9559ad822c"),
				AppUserId = Guid.Parse("9bdbf5e7-ad83-415c-b359-9ff5e2f0ded1"),
				Score = 1
			};
			var barRating5 = new BarRating
			{
				BarId = Guid.Parse("0899e918-977c-44d5-a5cb-de9559ad822c"),
				AppUserId = Guid.Parse("9bdbf5e7-ad83-415c-b359-9ff5e2f0ded2"),
				Score = 3
			};

			//Ratings for Test Bar1 - AvgRating 5
			var barRating6 = new BarRating
			{
				BarId = Guid.Parse("0899e918-977c-44d5-a5cb-de9559ad822a"),
				AppUserId = Guid.Parse("9bdbf5e7-ad83-415c-b359-9ff5e2f0ded1"),
				Score = 5
			};

			using (var arrangeContext = new CMContext(options))
			{
				await arrangeContext.BarRatings.AddAsync(barRating1);
				await arrangeContext.BarRatings.AddAsync(barRating2);
				await arrangeContext.BarRatings.AddAsync(barRating3);
				await arrangeContext.BarRatings.AddAsync(barRating4);
				await arrangeContext.BarRatings.AddAsync(barRating5);
				await arrangeContext.BarRatings.AddAsync(barRating6);
				await arrangeContext.SaveChangesAsync();
			}

			//Act/Assert
			using (var assertContext = new CMContext(options))
			{
				var sut = new BarServices(assertContext, mockMapper.Object, mockMapperAddress.Object);

				var result = await sut.GetTopBarsAsync(2);
				var resultList = result.ToList();

				Assert.AreEqual("Test Bar1", resultList[0].Name);
				Assert.AreEqual(5, resultList[0].AverageRating);
				Assert.AreEqual("Dante", resultList[1].Name);
				Assert.AreEqual(3, resultList[1].AverageRating);
			}
		}

	}
}
