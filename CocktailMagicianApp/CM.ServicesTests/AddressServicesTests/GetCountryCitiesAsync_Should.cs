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
	public class GetCountryCitiesAsync_Should
	{
		//[TestMethod]
		//public ReturnListOfCitiesDto_ValidInput()
		//{
		//	var options = Utility.GetOptions(nameof(ReturnListOfCitiesDto_ValidInput));
		//	Utility.ArrangeContextAsync(options);

		//	var mockMapper = new Mock<IAddressMapper>();
		//	mockMapper.Setup(x => x.CreateCityDTO(It.IsAny<City>()))
		//			  .Returns<City>(city => new CityDTO
		//			  {
		//				  Id = city.Id,
		//				  Name = city.Name
		//			  });

		//	using var assertContext = new CMContext(options);

		//	var sut = new AddressServices(assertContext, mockMapper.Object);

		//	var result = await sut.GetCountryCitiesAsync();
		//	var expected = await assertContext.Countries.ToListAsync();
		//	Assert.AreEqual(expected.Count, result.Count);
		//}
	}
}
