using CM.Data;
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
    public class PageCocktailsAsync_Should
    {
        [TestMethod]
        public async Task ReturnCorrect_When_UnlistedNotAllowed_And_PaginationNotDefault()
        {
            var options = Utility.GetOptions(nameof(ReturnCorrect_When_UnlistedNotAllowed_And_PaginationNotDefault));
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
                                             IngredientName = i.Ingredient?.Name
                                         })
                                         .ToList(),
                      });

            var pageNumber = 2;
            var pageSize = 1;

            using var assertContext = new CMContext(options);
            {
                var sut = new CocktailServices(assertContext, mockMapper.Object);
                var result = await sut.PageCocktailsAsync(pageNumber: pageNumber, pageSize: pageSize, allowUnlisted: false);
                Assert.AreEqual(pageSize, result.Count);
                Assert.AreEqual("Cocktail B", result[0].Name);
            }
        }

        [TestMethod]
        public async Task Return_All_When_UnlistedAllowed()
        {
            var options = Utility.GetOptions(nameof(Return_All_When_UnlistedAllowed));
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
                                             IngredientName = i.Ingredient?.Name
                                         })
                                         .ToList(),
                      });

            var pageNumber = 1; // default value for optional parameter
            var pageSize = 10; // default value for optional parameter

            using var assertContext = new CMContext(options);
            {
                var sut = new CocktailServices(assertContext, mockMapper.Object);
                var result = await sut.PageCocktailsAsync(pageNumber: pageNumber, pageSize: pageSize, allowUnlisted: true);
                Assert.IsTrue(result.Count <= pageSize);
                Assert.IsTrue(result.Any(c => c.IsUnlisted == true));
            }
        }

        [TestMethod]
        public async Task ReturnFiltered_When_SearchStringProvided()
        {
            var options = Utility.GetOptions(nameof(ReturnFiltered_When_SearchStringProvided));
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
                                             IngredientName = i.Ingredient?.Name
                                         })
                                         .ToList(),
                      });

            var search = "B";

            using var assertContext = new CMContext(options);
            {
                var sut = new CocktailServices(assertContext, mockMapper.Object);
                var result = await sut.PageCocktailsAsync(searchString: search);
                var expected = await assertContext.Cocktails.Where(c => c.Name.Contains(search)
                                                                        && !c.IsUnlisted)
                                                            .ToListAsync();
                Assert.AreEqual(expected.Count, result.Count);
                Assert.AreEqual(expected[0].Id, result[0].Id);
            }
        }

        [TestMethod]
        public async Task Return_SortedBy_RatingAsc()
        {
            var options = Utility.GetOptions(nameof(Return_SortedBy_RatingAsc));
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
                                             IngredientName = i.Ingredient?.Name
                                         })
                                         .ToList(),
                      });

            var sortBy = "rating";

            using var assertContext = new CMContext(options);
            {
                var sut = new CocktailServices(assertContext, mockMapper.Object);
                var result = await sut.PageCocktailsAsync(sortBy: sortBy, allowUnlisted: true);
                for (int i = 1; i < result.Count; i++)
                {
                    Assert.IsTrue((result[i - 1].AverageRating ?? 0) <= (result[i].AverageRating ?? 0));
                }
            }
        }

        [TestMethod]
        public async Task Return_SortedBy_RatingDesc()
        {
            var options = Utility.GetOptions(nameof(Return_SortedBy_RatingDesc));
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
                                             IngredientName = i.Ingredient?.Name
                                         })
                                         .ToList(),
                      });

            var sortBy = "rating";
            var sortOrder = "desc";

            using var assertContext = new CMContext(options);
            {
                var sut = new CocktailServices(assertContext, mockMapper.Object);
                var result = await sut.PageCocktailsAsync(sortBy: sortBy, sortOrder: sortOrder, allowUnlisted: true);
                for (int i = 1; i < result.Count; i++)
                {
                    Assert.IsTrue((result[i - 1].AverageRating ?? 0) >= (result[i].AverageRating ?? 0));
                }
            }
        }

        [TestMethod]
        public async Task Return_SortedBy_NameAsc()
        {
            var options = Utility.GetOptions(nameof(Return_SortedBy_NameAsc));
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
                                             IngredientName = i.Ingredient?.Name
                                         })
                                         .ToList(),
                      });

            var sortBy = "name";

            using var assertContext = new CMContext(options);
            {
                var sut = new CocktailServices(assertContext, mockMapper.Object);
                var result = await sut.PageCocktailsAsync(sortBy: sortBy, allowUnlisted: true);
                for (int i = 1; i < result.Count; i++)
                {
                    Assert.AreEqual(-1, string.Compare(result[i - 1].Name, result[i].Name, StringComparison.InvariantCultureIgnoreCase));
                }
            }
        }

        [TestMethod]
        public async Task Return_SortedBy_NameDesc()
        {
            var options = Utility.GetOptions(nameof(Return_SortedBy_NameDesc));
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
                                             IngredientName = i.Ingredient?.Name
                                         })
                                         .ToList(),
                      });

            var sortBy = "name";
            var sortOrder = "desc";

            using var assertContext = new CMContext(options);
            {
                var sut = new CocktailServices(assertContext, mockMapper.Object);
                var result = await sut.PageCocktailsAsync(sortBy: sortBy, sortOrder: sortOrder, allowUnlisted: true);
                for (int i = 1; i < result.Count; i++)
                {
                    Assert.AreEqual(1, string.Compare(result[i - 1].Name, result[i].Name, StringComparison.InvariantCultureIgnoreCase));
                }
            }
        }
    }
}
