using System;
using System.Linq;
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
	public class GetCountryCitiesAsync_Should
	{
        [TestMethod]
        public async Task ReturnListOfCitiesDto_ValidInput()
        {
            //Arrange
            var options = Utility.GetOptions(nameof(ReturnListOfCitiesDto_ValidInput));
            await Utility.ArrangeContextAsync(options);

            var mockMapper = new Mock<IAddressMapper>();
            mockMapper.Setup(x => x.CreateCityDTO(It.IsAny<City>()))
                      .Returns<City>(city => new CityDTO
                      {
                          Id = city.Id,
                          Name = city.Name
                      });

            var countryId = Guid.Parse("4828b9db-cd3a-487f-9782-7a23653ff99a");

            //Act/Assert
            using (var assertContext = new CMContext(options))
            {
                var sut = new AddressServices(assertContext, mockMapper.Object);

                var result = sut.GetCountryCitiesAsync(countryId);
                var expected = await assertContext.Cities.Where(city => city.CountryId == countryId).ToListAsync();
                Assert.AreEqual(expected.Count, result.Count);
            }
        }

        public async Task ReturnEmptyCollection_forCountryWithNoCities()
        {
            //Arrange
            var options = Utility.GetOptions(nameof(ReturnListOfCitiesDto_ValidInput));
            await Utility.ArrangeContextAsync(options);

            var mockMapper = new Mock<IAddressMapper>();
            mockMapper.Setup(x => x.CreateCityDTO(It.IsAny<City>()))
                      .Returns<City>(city => new CityDTO
                      {
                          Id = city.Id,
                          Name = city.Name
                      });

            var countryId = Guid.Parse("fb4effe9-32c1-45fd-8fce-9b45259c7ff6");

            //Act/Assert
            using (var assertContext = new CMContext(options))
            {
                var sut = new AddressServices(assertContext, mockMapper.Object);

                var result = sut.GetCountryCitiesAsync(countryId);
                Assert.AreEqual(0, result.Count);
            }
        }
    }
}
