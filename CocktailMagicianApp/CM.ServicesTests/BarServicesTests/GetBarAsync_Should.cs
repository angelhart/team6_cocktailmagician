using System;
using System.Collections.Generic;
using System.Text;
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
	public class GetBarAsync_Should
	{
		[TestMethod]
		public async Task ReturnBarDto_WhenIdFound()
		{
			//Arrange
			var options = Utility.GetOptions(nameof(ReturnBarDto_WhenIdFound));

			var bar = new Bar
			{
				Id = new Guid("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd"),
				Name = "Dante",
			};


			var mockMapper = new Mock<IBarMapper>();
			mockMapper.Setup(x => x.CreateBarDTO(It.IsAny<Bar>()))
					  .Returns<Bar>(bar => new BarDTO
					  {
						  Id = bar.Id,
						  Name = bar.Name
					  });

			var mockMapperAddress = new Mock<IAddressServices>();
			mockMapperAddress.Setup(x => x.CreateAddressAsync(It.IsAny<AddressDTO>()));


			var lookUpId = Guid.Parse("9bdbf5e7-ad83-415c-b359-9ff5e2f0dedd");

			using (var arrangeContext = new CMContext(options))
			{
				await arrangeContext.AddAsync(bar);
				await arrangeContext.SaveChangesAsync();
			};


			//Act/Assert
			using (var assertContext = new CMContext(options))
			{
				var sut = new BarServices(assertContext, mockMapper.Object, mockMapperAddress.Object);

				var result = await sut.GetBarAsync(lookUpId);
				var expected = await assertContext.Countries.FindAsync(lookUpId);
				Assert.AreEqual(expected.Name, result.Name);
			}
		}

		[TestMethod]
		public async Task ThrowArgumentException_WhenBar_NotFound()
		{
			//Arrange
			var options = Utility.GetOptions(nameof(ThrowArgumentException_WhenBar_NotFound));

			var mockMapper = new Mock<IBarMapper>();
			var mockMapperAddress = new Mock<IAddressServices>();


			var lookUpId = Guid.Parse("00000000-0000-0000-0000-000000000000");

			//Act/Assert
			using (var assertContext = new CMContext(options))
			{
				var sut = new BarServices(assertContext, mockMapper.Object, mockMapperAddress.Object);

				await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.GetBarAsync(lookUpId));
			}
		}
	}
}
