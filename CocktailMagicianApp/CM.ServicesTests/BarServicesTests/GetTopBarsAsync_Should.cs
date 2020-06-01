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
