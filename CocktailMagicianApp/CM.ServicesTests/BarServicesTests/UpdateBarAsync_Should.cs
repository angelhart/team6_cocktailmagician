using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers.Contracts;
using CM.Models;
using CM.Services;
using CM.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CM.ServicesTests.BarServicesTests
{
	[TestClass]
	public class UpdateBarAsync_Should
	{
		[TestMethod]
		public async Task ReturnCorrectBarDTOAfterUpdate_ValidParams()
		{
			//Arrange
			var options = Utility.GetOptions(nameof(ReturnCorrectBarDTOAfterUpdate_ValidParams));

			var bar = new Bar
			{
				Id = new Guid("ff3a5da3-060e-4baa-99d9-372d5701d5f1"),
				Name = "TestBar",
				Phone = "(000) 00-0000",
				Details = "Test Details",
			};

			var barDTO = new BarDTO
			{
				Id = new Guid("ff3a5da3-060e-4baa-99d9-372d5701d5f1"),
				Name = "TestBar-updated",
				Phone = "(000) 00-0002",
				Details = "Test Details-updated",

				Cocktails = new List<CocktailDTO> {new CocktailDTO
				{
					Id = new Guid("a3fd2a00-52c4-4293-a184-6f448d008015"),
				}, }
			};

			var mockMapper = new Mock<IBarMapper>();
			mockMapper.Setup(x => x.CreateBarDTO(It.IsAny<Bar>()))
					  .Returns<Bar>(bar => new BarDTO
					  {
						  Id = bar.Id,
						  Name = bar.Name,
						  Phone = bar.Phone,
						  Details = bar.Details
					  });

			var mockMapperAddress = new Mock<IAddressServices>();

			using (var arrangeContext = new CMContext(options))
			{
				await arrangeContext.Bars.AddAsync(bar);
				await arrangeContext.SaveChangesAsync();
			}

			//Act/Assert
			using (var assertContext = new CMContext(options))
			{
				var sut = new BarServices(assertContext, mockMapper.Object, mockMapperAddress.Object);

				var result = await sut.UpdateBarAsync(new Guid("ff3a5da3-060e-4baa-99d9-372d5701d5f1"), barDTO);
				var expected = await assertContext.Bars.FirstOrDefaultAsync(bar => bar.Id == new Guid("ff3a5da3-060e-4baa-99d9-372d5701d5f1"));

				Assert.AreEqual(expected.Name, result.Name);
				Assert.AreEqual(expected.Phone, result.Phone);
				Assert.AreEqual(expected.Details, result.Details);
			}
		}

		[TestMethod]
		public async Task ThrowsArgumentNullException_WhenBarIsNotFound()
		{
			//Arrange
			var options = Utility.GetOptions(nameof(ThrowsArgumentNullException_WhenBarIsNotFound));
			await Utility.ArrangeContextAsync(options);

			var barDTO = new BarDTO
			{
				Id = new Guid("ff3a5da3-060e-4baa-99d9-372d5701d5f1"),
				Name = "TestBar-updated",
				Phone = "(000) 00-0002",
				Details = "Test Details-updated",
			};

			var mockMapper = new Mock<IBarMapper>();
			var mockMapperAddress = new Mock<IAddressServices>();

			//Act/Assert
			using (var assertContext = new CMContext(options))
			{
				var sut = new BarServices(assertContext, mockMapper.Object, mockMapperAddress.Object);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>sut.UpdateBarAsync(new Guid("ff3a5da3-060e-4baa-99d9-372d5701d5f1"), barDTO));
			}
		}

		[TestMethod]
		public async Task ThrowsArgumentNullException_WhenBarDTOIsNull()
		{
			//Arrange
			var options = Utility.GetOptions(nameof(ThrowsArgumentNullException_WhenBarDTOIsNull));
			await Utility.ArrangeContextAsync(options);

			var mockMapper = new Mock<IBarMapper>();
			var mockMapperAddress = new Mock<IAddressServices>();

			//Act/Assert
			using (var assertContext = new CMContext(options))
			{
				var sut = new BarServices(assertContext, mockMapper.Object, mockMapperAddress.Object);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.UpdateBarAsync(new Guid("ff3a5da3-060e-4baa-99d9-372d5701d5f1"), null));
			}
		}
	}
}
