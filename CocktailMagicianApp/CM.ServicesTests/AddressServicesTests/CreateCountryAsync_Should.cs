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
	public class CreateCountryAsync_Should
	{
		[TestMethod]
		public async Task ReturnCorrectCountryAfterAdd_ValidParams()
		{
			//Arrange
			var options = Utility.GetOptions(nameof(ReturnCorrectCountryAfterAdd_ValidParams));

			var newCountryDTO = new CountryDTO
			{
				Name = "TestCountry"
			};

			var mockMapper = new Mock<IAddressMapper>();
			mockMapper.Setup(x => x.CreateCountryDTO(It.IsAny<Country>()))
					  .Returns<Country>(country => new CountryDTO
					  {
						  Id = country.Id,
						  Name = country.Name
					  });

			//Act/Assert
			using (var assertContext = new CMContext(options))
			{
				var sut = new AddressServices(assertContext, mockMapper.Object);

				var result = await sut.CreateCountryAsync(newCountryDTO);
				var expected = await assertContext.Countries.ToListAsync();
				Assert.AreEqual(expected.Count, 1);
				Assert.AreEqual(expected[0].Name, result.Name);
			}
		}

		[TestMethod]
		public async Task ThrowsArgumentNullException_InValidParams()
		{
			//Arrange
			var options = Utility.GetOptions(nameof(ThrowsArgumentNullException_InValidParams));

			var newCountryDTO = new CountryDTO 
			{
				Name = null
			};

			var mockMapper = new Mock<IAddressMapper>();
			mockMapper.Setup(x => x.CreateCountryDTO(It.IsAny<Country>()))
					  .Returns<Country>(country => new CountryDTO
					  {
						  Id = country.Id,
						  Name = country.Name
					  });

			//Act/Assert
			using (var assertContext = new CMContext(options))
			{
				var sut = new AddressServices(assertContext, mockMapper.Object);

				await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.CreateCountryAsync(newCountryDTO));
			}
		}

		[TestMethod]
		public async Task ThrowsDbUpdateException_WhenCountryExists()
		{
			//Arrange
			var options = Utility.GetOptions(nameof(ThrowsDbUpdateException_WhenCountryExists));

			var newCountryDTO = new CountryDTO
			{
				Name = "TestCountry"
			};

			var existingCountry = new Country
			{
				Name = "TestCountry"
			};

			var mockMapper = new Mock<IAddressMapper>();
			mockMapper.Setup(x => x.CreateCountryDTO(It.IsAny<Country>()))
					  .Returns<Country>(country => new CountryDTO
					  {
						  Id = country.Id,
						  Name = country.Name
					  });

			using (var arrangeContext = new CMContext(options))
			{
				await arrangeContext.Countries.AddAsync(existingCountry);
				await arrangeContext.SaveChangesAsync();
			}

			//Act/Assert
			using (var assertContext = new CMContext(options))
			{
				var sut = new AddressServices(assertContext, mockMapper.Object);

				await Assert.ThrowsExceptionAsync<DbUpdateException>(() => sut.CreateCountryAsync(newCountryDTO));
			}
		}

	}
}
