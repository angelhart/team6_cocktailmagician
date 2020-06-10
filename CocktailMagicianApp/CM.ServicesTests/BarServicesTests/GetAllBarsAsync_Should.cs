using System;
using System.Linq;
using System.Threading.Tasks;
using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers.Contracts;
using CM.Models;
using CM.Services;
using CM.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CM.ServicesTests.BarServicesTests
{
	[TestClass]
	public class GetAllBarsAsync_Should
	{
        [TestMethod]
        public async Task ReturnWithoutUnlisted()
        {
            var options = Utility.GetOptions(nameof(ReturnWithoutUnlisted));
            await Utility.ArrangeContextAsync(options);

            var mockAddressServices = new Mock<IAddressServices>();
            var mockMapper = new Mock<IBarMapper>();
            mockMapper.Setup(x => x.CreateBarDTO(It.IsAny<Bar>()))
                          .Returns<Bar>(bar => new BarDTO
                          {
                              Id = bar.Id,
                              Name = bar.Name,
                              IsUnlisted = bar.IsUnlisted
                          });

            var pageNumber = 1;
            var pageSize = 1;

            using var assertContext = new CMContext(options);
            {
                var sut = new BarServices(assertContext, mockMapper.Object, mockAddressServices.Object);
                var result = await sut.GetAllBarsAsync(pageNumber: pageNumber, pageSize: pageSize, allowUnlisted: false);
                Assert.AreEqual(pageSize, result.Count);
                Assert.AreEqual("Bar A", result[0].Name);
            }
        }

        [TestMethod]
        public async Task ReturnWithUnlisted()
        {
            var options = Utility.GetOptions(nameof(ReturnWithUnlisted));
            await Utility.ArrangeContextAsync(options);

            var mockAddressServices = new Mock<IAddressServices>();
            var mockMapper = new Mock<IBarMapper>();
            mockMapper.Setup(x => x.CreateBarDTO(It.IsAny<Bar>()))
                          .Returns<Bar>(bar => new BarDTO
                          {
                              Id = bar.Id,
                              Name = bar.Name,
                              IsUnlisted = bar.IsUnlisted
                          });

            var pageNumber = 1; 
            var pageSize = 10; 

            using var assertContext = new CMContext(options);
            {
                var sut = new BarServices(assertContext, mockMapper.Object, mockAddressServices.Object);
                var result = await sut.GetAllBarsAsync(pageNumber: pageNumber, pageSize: pageSize, allowUnlisted: true);
                Assert.IsTrue(result.Any(b => b.IsUnlisted == true));
            }
        }
    }
}
