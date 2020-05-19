﻿using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers.Contracts;
using CM.Models;
using CM.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.ServicesTests.CocktailServicesTests
{
    [TestClass]
    public class GetTopCocktailsAsync_Should
    {
        [TestMethod]
        public async Task ReturnCollection_When_Requested()
        {
            var options = Utility.GetOptions(nameof(ReturnCollection_When_Requested));
            await Utility.ArrangeContextAsync(options);

            var mockMapper = new Mock<ICocktailMapper>();

            mockMapper.Setup(x => x.CreateCocktailDTO(It.IsAny<Cocktail>()))
                      .Returns<Cocktail>(c => new CocktailDTO
                      {
                          Id = c.Id,
                          Name = c.Name,
                          Recipe = c.Recipe,
                          IsUnlisted = c.IsUnlisted,
                          AverageRating = c.AverageRating,
                          Ingredients = c.Ingredients
                                         .Select(i => new CocktailIngredientDTO
                                         {
                                             IngredientId = i.IngredientId,
                                         })
                                         .ToList(),
                      });


            using var assertContext = new CMContext(options);
            {
                var sut = new CocktailServices(assertContext, mockMapper.Object);
                var result = sut.GetTopCocktailsAsync().Result.ToList();
                var listedCocktailsCount = assertContext.Cocktails.Count(c => !c.IsUnlisted);
                Assert.AreEqual(listedCocktailsCount, result.Count);
                Assert.IsTrue(result[0].AverageRating >= result[1].AverageRating);
            }
        }
    }
}