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
	public class GetAllCountriesAsync_Should
	{
		[TestMethod]
		public async Task ReturnListCountryyDto()
		{
			//Arrange
			var options = Utility.GetOptions(nameof(ReturnListCountryyDto));
			await Utility.ArrangeContextAsync(options);

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

				var result = await sut.GetAllCountriesAsync();
				var expected = await assertContext.Countries.ToListAsync();
				Assert.AreEqual(expected.Count, result.Count);
			}
		}
	}
}
