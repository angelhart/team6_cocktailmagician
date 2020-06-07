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
	public class CreateBarAsync_Should
	{
		[TestMethod]
		public async Task ReturnCorrectBarAfterAdd_ValidParams()
		{
			//Arrange
			var options = Utility.GetOptions(nameof(ReturnCorrectBarAfterAdd_ValidParams));

			var barDTO = new BarDTO
			{
				Id = new Guid("ff3a5da3-060e-4baa-99d9-372d5701d5f1"),
				Name = "TestBar",
				Phone = "(000) 00-0000",
				Details = "Test Details",

				Address = new AddressDTO
				{
					Id = new Guid("ff3a5da3-060e-4baa-99d9-372d5701d5f1"),
					CityId = new Guid("320b050b-82f1-494c-9add-91ab28bf98dd"),
					Street = "79-81 MACDOUGAL ST",
					BarId = Guid.Parse("ff3a5da3-060e-4baa-99d9-372d5701d5f1")
				},

				Cocktails = new List<CocktailDTO> 
				{
					new CocktailDTO	{ Id = new Guid("a3fd2a00-52c4-4293-a184-6f448d008015") }, 
				}
			};

			var mockMapper = new Mock<IBarMapper>();
			mockMapper.Setup(x => x.CreateBarDTO(It.IsAny<Bar>()))
					  .Returns<Bar>(bar => new BarDTO
					  {
						  Id = bar.Id,
						  Name = bar.Name
					  });

			var mockMapperAddress = new Mock<IAddressServices>();
			mockMapperAddress.Setup(x => x.CreateAddressAsync(It.IsAny<AddressDTO>()))
							.ReturnsAsync(new AddressDTO
							{
								Id = new Guid("ff3a5da3-060e-4baa-99d9-372d5701d5f1"),
								CityId = new Guid("320b050b-82f1-494c-9add-91ab28bf98dd"),
								Street = "79-81 MACDOUGAL ST",
								BarId = Guid.Parse("ff3a5da3-060e-4baa-99d9-372d5701d5f1")
							});

			//Act/Assert
			using (var assertContext = new CMContext(options))
			{
				var sut = new BarServices(assertContext, mockMapper.Object, mockMapperAddress.Object);

				var result = await sut.CreateBarAsync(barDTO);
				var expected = await assertContext.Bars.ToListAsync();

				Assert.AreEqual(expected[0].Name, result.Name);
			}
		}

		[TestMethod]
		public async Task ThrowsArgumentNullException_BarNameIsNull()
		{
			//Arrange
			var options = Utility.GetOptions(nameof(ThrowsArgumentNullException_BarNameIsNull));
			await Utility.ArrangeContextAsync(options);

			var barDTO = new BarDTO
			{
				Name = null,
				Phone = "(000) 00-0000",
				Details = "Test Details",
			};

			var mockMapper = new Mock<IBarMapper>();
			var mockMapperAddress = new Mock<IAddressServices>();

			//Act/Assert
			using (var assertContext = new CMContext(options))
			{
				var sut = new BarServices(assertContext, mockMapper.Object, mockMapperAddress.Object);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.CreateBarAsync(barDTO));
			}
		}

		[TestMethod]
		public async Task ThrowsDbUpdateException_WhenBarExists()
		{
			//Arrange
			var options = Utility.GetOptions(nameof(ThrowsDbUpdateException_WhenBarExists));

			var barDTO = new BarDTO
			{
				Name = "TestBar"
			};


			var existingBar = new Bar
			{
				Name = "TestBar"
			};

			var mockMapper = new Mock<IBarMapper>();
			var mockMapperAddress = new Mock<IAddressServices>();

			using (var arrangeContext = new CMContext(options))
			{
				await arrangeContext.Bars.AddAsync(existingBar);
				await arrangeContext.SaveChangesAsync();
			}

			//Act/Assert
			using (var assertContext = new CMContext(options))
			{
				var sut = new BarServices(assertContext, mockMapper.Object, mockMapperAddress.Object);

				await Assert.ThrowsExceptionAsync<DbUpdateException>(() => sut.CreateBarAsync(barDTO));
			}
		}

	}
}
