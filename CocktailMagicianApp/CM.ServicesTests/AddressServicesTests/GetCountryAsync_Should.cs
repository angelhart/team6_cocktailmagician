using System;
using System.Threading.Tasks;
using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers.Contracts;
using CM.Models;
using CM.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CM.ServicesTests.AddressServicesTests
{
	[TestClass]
	public class GetCountryAsync_Should
	{
        [TestMethod]
        public async Task ReturnCountryyDto_WhenIdFound()
        {
            var options = Utility.GetOptions(nameof(ReturnCountryyDto_WhenIdFound));
            await Utility.ArrangeContextAsync(options);

            var mockMapper = new Mock<IAddressMapper>();
            mockMapper.Setup(x => x.CreateCountryDTO(It.IsAny<Country>()))
                      .Returns<Country>(country => new CountryDTO
                      {
                          Id = country.Id,
                          Name = country.Name
                      });

            var lookUpId = Guid.Parse("4828b9db-cd3a-487f-9782-7a23653ff99a");

            using var assertContext = new CMContext(options);

            var sut = new AddressServices(assertContext, mockMapper.Object);

            var result = await sut.GetCountryAsync(lookUpId);
            var expected = await assertContext.Countries.FindAsync(lookUpId);
            Assert.AreEqual(expected.Name, result.Name);
        }

        [TestMethod]
        public async Task ThrowArgumentException_WhenCountry_NotFound()
        {
            var options = Utility.GetOptions(nameof(ThrowArgumentException_WhenCountry_NotFound));

            var mockMapper = new Mock<IAddressMapper>();

            var lookUpId = Guid.Parse("00000000-0000-0000-0000-000000000000");

            using var assertContext = new CMContext(options);

            var sut = new AddressServices(assertContext, mockMapper.Object);

            await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.GetCountryAsync(lookUpId));
        }
    }
}
