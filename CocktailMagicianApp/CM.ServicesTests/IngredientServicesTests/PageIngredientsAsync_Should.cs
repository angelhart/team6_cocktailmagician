﻿using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers.Contracts;
using CM.Models;
using CM.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace CM.ServicesTests.IngredientServicesTests
{
    [TestClass]
    public class PageIngredientsAsync_Should
    {
        [TestMethod]
        public async Task Throw_When_NullSearchString_Ingr()
        {
            var options = Utility.GetOptions(nameof(Throw_When_NullSearchString_Ingr));
            await Utility.ArrangeContextAsync(options);

            var mockMapper = new Mock<IIngredientMapper>();

            using var assertContext = new CMContext(options);
            {
                var sut = new IngredientServices(assertContext, mockMapper.Object);
                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.PageIngredientsAsync(searchString: null));
            }
        }

        [TestMethod]
        public async Task Throw_When_PageNumberInvalid_Ingr()
        {
            var options = Utility.GetOptions(nameof(Throw_When_PageNumberInvalid_Ingr));
            await Utility.ArrangeContextAsync(options);

            var mockMapper = new Mock<IIngredientMapper>();

            using var assertContext = new CMContext(options);
            {
                var sut = new IngredientServices(assertContext, mockMapper.Object);
                await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => sut.PageIngredientsAsync(pageNumber: 0));
            }
        }

        public async Task Throw_When_PageSizeInvalid_Ingr()
        {
            var options = Utility.GetOptions(nameof(Throw_When_PageSizeInvalid_Ingr));
            await Utility.ArrangeContextAsync(options);

            var mockMapper = new Mock<IIngredientMapper>();

            using var assertContext = new CMContext(options);
            {
                var sut = new IngredientServices(assertContext, mockMapper.Object);
                await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => sut.PageIngredientsAsync(pageSize: 0));
            }
        }

        [TestMethod]
        public async Task ReturnAllPaged_When_NoSearchStringProvided()
        {
            var options = Utility.GetOptions(nameof(ReturnAllPaged_When_NoSearchStringProvided));
            await Utility.ArrangeContextAsync(options);

            var mockMapper = new Mock<IIngredientMapper>();
            mockMapper.Setup(x => x.CreateIngredientDTO(It.IsAny<Ingredient>()))
                      .Returns<Ingredient>(i => new IngredientDTO
                      {
                          Id = i.Id,
                          Name = i.Name
                      });

            using var assertContext = new CMContext(options);

            var sut = new IngredientServices(assertContext, mockMapper.Object);

            var result = await sut.PageIngredientsAsync();
            var expected = await assertContext.Ingredients.ToListAsync();

            Assert.AreNotEqual(0, expected.Count);
            Assert.AreEqual(expected.Count, result.Count);
        }

        [TestMethod]
        public async Task ReturnOnePerPage_When_NoSearchStringProvided()
        {
            var options = Utility.GetOptions(nameof(ReturnOnePerPage_When_NoSearchStringProvided));
            await Utility.ArrangeContextAsync(options);

            var mockMapper = new Mock<IIngredientMapper>();
            mockMapper.Setup(x => x.CreateIngredientDTO(It.IsAny<Ingredient>()))
                      .Returns<Ingredient>(i => new IngredientDTO
                      {
                          Id = i.Id,
                          Name = i.Name
                      });

            var pageSize = 1;
            var pageNumber = 2;

            using var assertContext = new CMContext(options);

            var sut = new IngredientServices(assertContext, mockMapper.Object);

            var result = await sut.PageIngredientsAsync(searchString: "", pageNumber: pageNumber, pageSize: pageSize);
            var expected = await assertContext.Ingredients.ToListAsync();

            Assert.AreEqual(pageSize, result.Count);
            Assert.AreEqual(expected[pageNumber-1].Name, result[0].Name);
        }

        [TestMethod]
        public async Task ReturnEmptyCollection_WhenNoIngredientsPresent()
        {
            var options = Utility.GetOptions(nameof(ReturnEmptyCollection_WhenNoIngredientsPresent));

            var mockMapper = new Mock<IIngredientMapper>();
            mockMapper.Setup(x => x.CreateIngredientDTO(It.IsAny<Ingredient>()))
                      .Returns<Ingredient>(i => new IngredientDTO
                      {
                          Id = i.Id,
                          Name = i.Name
                      });

            using var assertContext = new CMContext(options);

            var sut = new IngredientServices(assertContext, mockMapper.Object);

            var result = await sut.PageIngredientsAsync();
            var expected = await assertContext.Ingredients.ToListAsync();
            Assert.AreEqual(0, expected.Count);
            Assert.AreEqual(expected.Count, result.Count);
        }
    }
}
