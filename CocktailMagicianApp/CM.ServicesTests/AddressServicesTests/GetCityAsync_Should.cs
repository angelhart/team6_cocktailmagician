﻿using System;
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
	public class GetCityAsync_Should
	{
		[TestMethod]
		public async Task ReturnCityDto_WhenIdFound()
		{
			//Arrange
			var options = Utility.GetOptions(nameof(ReturnCityDto_WhenIdFound));

			var existingCity = new City
			{
				Id = Guid.Parse("320b050b-82f1-494c-9add-91ab28bf98dd"),
				Name = "TestCity",
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
				await arrangeContext.Cities.AddAsync(existingCity);
				await arrangeContext.SaveChangesAsync();
			}

			var lookUpId = Guid.Parse("320b050b-82f1-494c-9add-91ab28bf98dd");

			//Act/Assert
			using (var assertContext = new CMContext(options))
			{
				var sut = new AddressServices(assertContext, mockMapper.Object);

				var result = await sut.GetCityAsync(lookUpId);
				var expected = await assertContext.Cities.FindAsync(lookUpId);
				Assert.AreEqual(expected.Name, result.Name);
			}
		}

		[TestMethod]
		public async Task ThrowArgumentException_WhenId_NotFound()
		{
			//Arrange
			var options = Utility.GetOptions(nameof(ThrowArgumentException_WhenId_NotFound));

			var mockMapper = new Mock<IAddressMapper>();

			var lookUpId = Guid.Parse("00000000-0000-0000-0000-000000000000");

			//Act/Assert
			using (var assertContext = new CMContext(options))
			{

				var sut = new AddressServices(assertContext, mockMapper.Object);

				await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.GetCityAsync(lookUpId));
			}
		}
	}
}
