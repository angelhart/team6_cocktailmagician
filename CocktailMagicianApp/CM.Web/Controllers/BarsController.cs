using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CM.Services.Contracts;
using CM.Web.Models;
using CM.DTOs;
using CM.DTOs.Mappers.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace CM.Web.Controllers
{
	public class BarsController : Controller
	{
		private readonly IBarServices _barServices;
		private readonly IAddressServices _addressServices;
		private readonly IAddressMapper _addressMapper;
		private readonly IRatingServices _ratingServices;
		private readonly IAppUserServices _appUserServices;

		public BarsController(IAppUserServices appUserServices,IBarServices barServices, IAddressServices addressServices, IAddressMapper addressMapper, IRatingServices ratingServices)
		{
			_barServices = barServices ?? throw new ArgumentNullException(nameof(barServices));
			_addressServices = addressServices ?? throw new ArgumentNullException(nameof(addressServices));
			_addressMapper = addressMapper ?? throw new ArgumentNullException(nameof(addressMapper));
			_ratingServices = ratingServices ?? throw new ArgumentNullException(nameof(ratingServices));
			_appUserServices = appUserServices ?? throw new ArgumentNullException(nameof(appUserServices));
		}

		public async Task<IActionResult> Index()
		{
			var bars = await this._barServices.GetAllBarsAsync();
			var barsViewModel = bars.Select(x => new BarIndexViewModel
			{
				Id = x.Id,
				Name = x.Name,
				Country = x.Address.CountryName,
				City = x.Address.CityName,
				AverageRating = x.AverageRating,
				ImagePath = x.ImagePath,
			});
			return View(barsViewModel);
		}

		public async Task<IActionResult> Details(Guid id)
		{

			if (id == null)
			{
				return NotFound();
			}

			var barDTO = await this._barServices.GetBarAsync(id);
			//TODO dto=>view model mappers
			var barViewModel = new BarViewModel
			{
				Id = barDTO.Id,
				Name = barDTO.Name,
				Country = barDTO.Address.CountryName,
				City = barDTO.Address.CityName,
				Street = barDTO.Address.Street,
				Phone = barDTO.Phone,
				Details = barDTO.Details,
				AverageRating = barDTO.AverageRating,
				ImagePath = barDTO.ImagePath,

				Cocktails = barDTO.Cocktails.Select(cocktailDTO => new CocktailViewModel
				{
					Id = cocktailDTO.Id,
					CocktailName = cocktailDTO.Name
				}).ToList()
			};

			return View(barViewModel);
		}

		public async Task<IActionResult> Create()
		{
			var collectionOfCountries = await  this._addressServices.GetAllCountriesAsync();

			var listOfCountries = collectionOfCountries.ToList();

			ViewBag.listOfCountries = listOfCountries;

			return View();
		}

		public JsonResult GetCountryCitiesAsync(Guid Id)
		{
			var collectionOfCities = this._addressServices.GetCountryCitiesAsync(Id);

			var listOfCities = collectionOfCities.ToList();

			return Json(listOfCities);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name, CityID, Street,Phone,ImagePath,Details")] BarViewModel barViewModel)
		{
			if (ModelState.IsValid)
			{
				try
				{
					//TODO View model to dto mapper
					var addressDTO = new AddressDTO
					{
						CityId = barViewModel.CityID,
						Street = barViewModel.Street,
					};

					var barDTO = new BarDTO
					{
						Id = barViewModel.Id,
						Name = barViewModel.Name,
						Phone = barViewModel.Phone,
						Details = barViewModel.Details,
						ImagePath = barViewModel.ImagePath,
						Address = addressDTO,
					};

					//TODO Ntoast notif
					await this._barServices.CreateBarAsync(barDTO);
					return RedirectToAction(nameof(Index));
				}
				catch (Exception)
				{
					return View(barViewModel);
					//TODO Ntoast notif
				}
			}
			return View(barViewModel);

		}

		public async Task<IActionResult> Edit(Guid id)
		{
			if (id == null)
			{
				return NotFound();
			}
			try
			{
				var barDTO = await this._barServices.GetBarAsync(id);

				var barViewModel = new BarViewModel
				{
					Id = barDTO.Id,
					Name = barDTO.Name,
					Phone = barDTO.Phone,
					Details = barDTO.Details,
					ImagePath = barDTO.ImagePath,
				};

				return View(barViewModel);
			}
			catch (Exception)
			{
				return NotFound();
			};
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Phone,ImagePath,Details")] BarViewModel barViewModel)
		{
			if (id != barViewModel.Id)
			{
				return NotFound();
			}

			var barDTO = new BarDTO
			{
				Id = barViewModel.Id,
				Name = barViewModel.Name,
				Phone = barViewModel.Phone,
				Details = barViewModel.Details,
				ImagePath = barViewModel.ImagePath,
			};
			//TODO add Ntoast notif
			if (ModelState.IsValid)
			{
				try
				{
					await _barServices.UpdateBarAsync(id, barDTO);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!BarExists(barViewModel.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(barViewModel);
		}
		public async Task<IActionResult> ChangeAddress(Guid barId)
		{

			if (barId == null)
			{
				return NotFound();
			}
			try
			{
				var barDTO = await this._barServices.GetBarAsync(barId);

				ViewData["Country"] = new SelectList(await this._addressServices.GetAllCountriesAsync(), "Id", "Name");
				//ViewData["City"] = new SelectList(await this._addressServices.GetCountryCities(ViewBag.Country), "Id", "Name");

				var barViewModel = new BarViewModel
				{
					Country = barDTO.Address.CountryName,
					City = barDTO.Address.CityName,
					Street = barDTO.Address.Street
				};

				return View(barViewModel);
			}
			catch (Exception)
			{
				return NotFound();
			};
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ChangeAddress(Guid id, [Bind("Id,Name,AddressID, CountryID,Phone,ImagePath,Details")] BarViewModel barViewModel)
		{
			if (id != barViewModel.Id)
			{
				return NotFound();
			}

			var barDTO = new BarDTO
			{
				Id = barViewModel.Id,
				Name = barViewModel.Name,
			};

			if (ModelState.IsValid)
			{
				try
				{
					await _barServices.UpdateBarAsync(id, barDTO);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!BarExists(barViewModel.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(barViewModel);
		}

		[HttpPost]
		//TODO [Authorize]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddRating(int barId, [Bind("Id,Score, barId")] RateBarViewModel rateBarViewModel)
		{
			if (ModelState.IsValid)
			{
				try
				{
					await _ratingServices.RateBarAsync(new BarRatingDTO
					{
						Score = rateBarViewModel.Score,
						AppUserId = rateBarViewModel.AppUserId,
						AppUserName = this.User.Identity.Name,
						BarId = rateBarViewModel.BarId
					});

					//TODO Ntoast notif - success
					return RedirectToAction(nameof(Details), new {id = barId});
				}
				catch (InvalidOperationException)
				{
					//TODO Ntoast notif when attempting to add another rating from same user
					return RedirectToAction(nameof(Details), new { id = barId });
				}
				catch
				{
					return NotFound();
				}
			}
			return RedirectToAction(nameof(Details), barId);
		}

		//public async Task<IActionResult> Delete(Guid? id)
		//{
		//    throw new NotImplementedException();

		//    //if (            id == null)
		//    //{
		//    //    return NotFound();
		//    //}

		//    //var bar = await _context.Bars
		//    //    .FirstOrDefaultAsync(m => m.Id == id);
		//    //if (bar == null)
		//    //{
		//    //    return NotFound();
		//    //}

		//    //return View(bar);
		//}

		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> DeleteConfirmed(Guid id)
		//{
		//    throw new NotImplementedException();

		//    //var bar = await _context.Bars.FindAsync(id);
		//    //_context.Bars.Remove(bar);
		//    //await _context.SaveChangesAsync();
		//    //return RedirectToAction(nameof(Index));
		//}

		private bool BarExists(Guid id)
		{
			throw new NotImplementedException();
			//return _context.Bars.Any(e => e.Id == id);
		}
	}
}
