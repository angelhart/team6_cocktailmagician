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
	public class CreateCityAsync_Should
	{
		[TestMethod]
		public async Task ReturnCorrectCityAfterAdd_ValidParams()
		{
			//Arrange
			var options = Utility.GetOptions(nameof(ReturnCorrectCityAfterAdd_ValidParams));

			var cityName = "TestCity";
			
			var country = new Country
			{
				Id = new Guid("fb4effe9-32c1-45fd-8fce-9b45259c7ff6"),
				Name = "Bulgaria"
			};

			var mockMapper = new Mock<IAddressMapper>();
			mockMapper.Setup(x => x.CreateCityDTO(It.IsAny<City>()))
					  .Returns<City>(city => new CityDTO
					  {
						  Id = city.Id,
						  Name = city.Name
					  });

			using (var arrangeContext = new CMContext(options))
			{
				await arrangeContext.Countries.AddAsync(country);
				await arrangeContext.SaveChangesAsync();
			}

			//Act/Assert
			using (var assertContext = new CMContext(options))
			{
				var sut = new AddressServices(assertContext, mockMapper.Object);

				var result = await sut.CreateCityAsync(cityName, new Guid("fb4effe9-32c1-45fd-8fce-9b45259c7ff6"));
				var expected = await assertContext.Cities.ToListAsync();

				Assert.AreEqual(expected.Count, 1);
				Assert.AreEqual(expected[0].Name, result.Name);
			}
		}

		[TestMethod]
		public async Task ThrowsArgumentNullException_CityNameIsNull()
		{
			//Arrange
			var options = Utility.GetOptions(nameof(ThrowsArgumentNullException_CityNameIsNull));

			string cityName = null;

			var country = new Country
			{
				Id = new Guid("fb4effe9-32c1-45fd-8fce-9b45259c7ff6"),
				Name = "Bulgaria"
			};

			var mockMapper = new Mock<IAddressMapper>();
			mockMapper.Setup(x => x.CreateCityDTO(It.IsAny<City>()))
					  .Returns<City>(city => new CityDTO
					  {
						  Id = city.Id,
						  Name = city.Name
					  });

			using (var arrangeContext = new CMContext(options))
			{
				await arrangeContext.Countries.AddAsync(country);
				await arrangeContext.SaveChangesAsync();
			}

			//Act/Assert
			using (var assertContext = new CMContext(options))
			{
				var sut = new AddressServices(assertContext, mockMapper.Object);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.CreateCityAsync(cityName, new Guid("fb4effe9-32c1-45fd-8fce-9b45259c7ff6")));
			}
		}

		[TestMethod]
		public async Task ThrowsDbUpdateException_WhenCityExists()
		{
			//Arrange
			var options = Utility.GetOptions(nameof(ThrowsDbUpdateException_WhenCityExists));

			var cityName = "TestCity";

			var existingCity = new City
			{
				Name = "TestCity",
				CountryId = new Guid("fb4effe9-32c1-45fd-8fce-9b45259c7ff6")
			};

			var country = new Country
			{
				Id = new Guid("fb4effe9-32c1-45fd-8fce-9b45259c7ff6"),
				Name = "Bulgaria"
			};

			var mockMapper = new Mock<IAddressMapper>();
			mockMapper.Setup(x => x.CreateCityDTO(It.IsAny<City>()))
					  .Returns<City>(city => new CityDTO
					  {
						  Id = city.Id,
						  Name = city.Name
					  });

			using (var arrangeContext = new CMContext(options))
			{
				await arrangeContext.Countries.AddAsync(country);
				await arrangeContext.Cities.AddAsync(existingCity);
				await arrangeContext.SaveChangesAsync();
			}

			//Act/Assert
			using (var assertContext = new CMContext(options))
			{
				var sut = new AddressServices(assertContext, mockMapper.Object);

				await Assert.ThrowsExceptionAsync<DbUpdateException>(() => sut.CreateCityAsync(cityName, new Guid("fb4effe9-32c1-45fd-8fce-9b45259c7ff6")));
			}
		}

		[TestMethod]
		public async Task ThrowsDbUpdateException_WhenCountryNotFound()
		{
			//Arrange
			var options = Utility.GetOptions(nameof(ThrowsDbUpdateException_WhenCityExists));

			var cityName = "TestCity";

			var mockMapper = new Mock<IAddressMapper>();
			mockMapper.Setup(x => x.CreateCityDTO(It.IsAny<City>()))
					  .Returns<City>(city => new CityDTO
					  {
						  Id = city.Id,
						  Name = city.Name
					  });

			//Act/Assert
			using (var assertContext = new CMContext(options))
			{
				var sut = new AddressServices(assertContext, mockMapper.Object);

				await Assert.ThrowsExceptionAsync<DbUpdateException>(() => sut.CreateCityAsync(cityName, new Guid("fb4effe9-32c1-45fd-8fce-9b45259c7ff6")));
			}
		}
	}
}
