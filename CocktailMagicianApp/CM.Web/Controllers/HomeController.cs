using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CM.Web.Models;
using CM.Services.Contracts;

namespace CM.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBarServices _barServices;
        private readonly ICocktailServices _cocktailServices;

		public HomeController(ILogger<HomeController> logger, IBarServices barServices, ICocktailServices cocktailServices)
		{
            _logger = logger;
            _barServices = barServices ?? throw new ArgumentNullException(nameof(barServices));
            _cocktailServices = cocktailServices ?? throw new ArgumentNullException(nameof(cocktailServices));
        }

        public async Task<IActionResult> Index()
        {
            var barModels = await this._barServices.GetTopBarsAsync(5);
            var topRatedBarsModels = barModels.Select(barDTO => new TopRatedBarsViewModel
            {
                Id = barDTO.Id,
                Name = barDTO.Name,
                Country = barDTO.Address.CountryName,
                City = barDTO.Address.CityName,
                ImagePath = barDTO.ImagePath,
                AverageRating = barDTO.AverageRating,
            }).ToList();

            var cocktailModels = await this._cocktailServices.GetTopCocktailsAsync(5);
            var topRatedCocktailsModels = cocktailModels.Select(cocktailDTO => new TopRatedCocktailsViewModel
            {
                Id = cocktailDTO.Id,
                Name = cocktailDTO.Name,
                ImagePath = cocktailDTO.ImagePath,
                AverageRating = cocktailDTO.AverageRating,
            }).ToList();

            var topModels = new TopRated();
            topModels.TopRatedBars = topRatedBarsModels;
            topModels.TopRatedCocktails = topRatedCocktailsModels;

            return View(topModels);
        }
        public IActionResult Missing()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
