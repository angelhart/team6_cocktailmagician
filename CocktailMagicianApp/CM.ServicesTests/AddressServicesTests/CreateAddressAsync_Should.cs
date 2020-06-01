using System;
using System.Threading.Tasks;
using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers.Contracts;
using CM.Models;
using CM.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CM.ServicesTests.AddressServicesTests
{
	[TestClass]
	public class CreateAddressAsync_Should
	{
		[TestMethod]
		public async Task ReturnAddressDto_AfterAdd_ValidParams()
		{
			//Arrange
			var options = Utility.GetOptions(nameof(ReturnAddressDto_AfterAdd_ValidParams));

			var newAddressDTO = new AddressDTO
			{
				CityId = Guid.Parse("320b050b-82f1-494c-9add-91ab28bf98dd"),
				Street = "Test Street"
			};

			var existingCity = new City
			{
				Id = Guid.Parse("320b050b-82f1-494c-9add-91ab28bf98dd"),
				Name = "TestCity",
			};

			var mockMapper = new Mock<IAddressMapper>();
			mockMapper.Setup(x => x.CreateAddressDTO(It.IsAny<Address>()))
					  .Returns<Address>(address => new AddressDTO
					  {
						  Id = address.Id,
						  Street = address.Street
					  });

			using (var arrangeContext = new CMContext(options))
			{
				await arrangeContext.Cities.AddAsync(existingCity);
				await arrangeContext.SaveChangesAsync();
			}

			var lookUpId = Guid.Parse("320b050b-82f1-494c-9add-91ab28bf98dd");

			//Act/Assert
			using (var assertContext = new CMContext(options))
			{
				var sut = new AddressServices(assertContext, mockMapper.Object);

				var result = await sut.CreateAddressAsync(newAddressDTO);
				var expected = await assertContext.Addresses.ToListAsync();
				Assert.AreEqual(expected[0].Street, result.Street);
			}
		}

		[TestMethod]
		public async Task ThrowsArgumentNullException_WhenStreetIsNull()
		{
			//Arrange
			var options = Utility.GetOptions(nameof(ThrowsArgumentNullException_WhenStreetIsNull));

			var newAddressDTO = new AddressDTO
			{
				CityId = Guid.Parse("320b050b-82f1-494c-9add-91ab28bf98dd"),
				Street = null
			};

			var existingCity = new City
			{
				Id = Guid.Parse("320b050b-82f1-494c-9add-91ab28bf98dd"),
				Name = "TestCity",
			};

			var mockMapper = new Mock<IAddressMapper>();
			mockMapper.Setup(x => x.CreateAddressDTO(It.IsAny<Address>()))
					  .Returns<Address>(address => new AddressDTO
					  {
						  Id = address.Id,
						  Street = address.Street
					  });

			using (var arrangeContext = new CMContext(options))
			{
				await arrangeContext.Cities.AddAsync(existingCity);
				await arrangeContext.SaveChangesAsync();
			}

			//Act/Assert
			using (var assertContext = new CMContext(options))
			{
				var sut = new AddressServices(assertContext, mockMapper.Object);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.CreateAddressAsync(newAddressDTO));
			}
		}

		[TestMethod]
		public async Task ThrowsDbUpdateException_WhenCityNotFound()
		{
			//Arrange
			var options = Utility.GetOptions(nameof(ThrowsDbUpdateException_WhenCityNotFound));

			var newAddressDTO = new AddressDTO
			{
				CityId = Guid.Parse("320b050b-82f1-494c-9add-91ab28bf98dd"),
				Street = "test street"
			};

			var mockMapper = new Mock<IAddressMapper>();
			mockMapper.Setup(x => x.CreateAddressDTO(It.IsAny<Address>()))
					  .Returns<Address>(address => new AddressDTO
					  {
						  Id = address.Id,
						  Street = address.Street
					  });

			var lookUpId = Guid.Parse("320b050b-82f1-494c-9add-91ab28bf98dd");

			//Act/Assert
			using (var assertContext = new CMContext(options))
			{
				var sut = new AddressServices(assertContext, mockMapper.Object);

				await Assert.ThrowsExceptionAsync<DbUpdateException>(() => sut.CreateAddressAsync(newAddressDTO));
			}
		}
	}
}
