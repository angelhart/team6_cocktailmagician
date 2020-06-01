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
using NToastNotify;
using CM.Models;

namespace CM.Web.Controllers
{
	public class BarsController : Controller
	{
		private readonly IBarServices _barServices;
		private readonly IAddressServices _addressServices;
		private readonly IAddressMapper _addressMapper;
		private readonly IRatingServices _ratingServices;
		private readonly ICommentServices _commentServices;
		private readonly IAppUserServices _appUserServices;
		private readonly IToastNotification _toastNotification;

		public BarsController(IAppUserServices appUserServices,IBarServices barServices, IAddressServices addressServices, IAddressMapper addressMapper, IRatingServices ratingServices,
			IToastNotification toastNotification, ICommentServices commentServices)
		{
			_barServices = barServices ?? throw new ArgumentNullException(nameof(barServices));
			_addressServices = addressServices ?? throw new ArgumentNullException(nameof(addressServices));
			_addressMapper = addressMapper ?? throw new ArgumentNullException(nameof(addressMapper));
			_ratingServices = ratingServices ?? throw new ArgumentNullException(nameof(ratingServices));
			_commentServices = commentServices ?? throw new ArgumentNullException(nameof(commentServices));
			_appUserServices = appUserServices ?? throw new ArgumentNullException(nameof(appUserServices));
			_toastNotification = toastNotification ?? throw new ArgumentNullException(nameof(toastNotification));
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

				Comments = barDTO.Comments.Select(barDTO => new BarCommentViewModel
				{
					Text = barDTO.Text,
					UserName = barDTO.UserName
				}).ToList(),

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

		[HttpGet]
		[Route("Create")]
		public async Task<IActionResult> GetCountryCitiesAsync(Guid Id)
		{
			var collectionOfCities = await this._addressServices.GetCountryCitiesAsync(Id);

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

					_toastNotification.AddSuccessToastMessage($"Bar {barDTO.Name} was successfully created!");
					await this._barServices.CreateBarAsync(barDTO);
					return RedirectToAction(nameof(Index));
				}
				catch (Exception)
				{
					_toastNotification.AddErrorToastMessage("Oops! Something went wrong!");
					return View(barViewModel);
				}
			}
			_toastNotification.AddErrorToastMessage("Oops! Something went wrong!");
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

			if (ModelState.IsValid)
			{
				try
				{
					await _barServices.UpdateBarAsync(id, barDTO);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (await BarExists(barViewModel.Id) == false)
					{
						return NotFound();
					}
					else
					{
						_toastNotification.AddErrorToastMessage("Oops! Something went wrong!");
						throw;
					}
				}
				_toastNotification.AddSuccessToastMessage($"Bar {barDTO.Name} was successfully updated!");
				return RedirectToAction(nameof(Index));
			}
			_toastNotification.AddErrorToastMessage("Oops! Something went wrong!");
			return View(barViewModel);
		}
		public async Task<IActionResult> ChangeAddress(Guid barId)
		{

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
					if (await BarExists(barViewModel.Id) == false)
					{
						return NotFound();
					}
					else
					{
						_toastNotification.AddErrorToastMessage("Oops! Something went wrong!");
						throw;
					}
				}
				_toastNotification.AddSuccessToastMessage($"The address of Bar {barDTO.Name} was successfully updated!");
				return RedirectToAction(nameof(Index));
			}
			_toastNotification.AddErrorToastMessage("Oops! Something went wrong!");
			return View(barViewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddRating([Bind("Score, BarId")] RateBarViewModel rateBarViewModel)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var appUserId = await this._appUserServices.GetUserIdAsync(User.Identity.Name);

					await _ratingServices.RateBarAsync(new BarRatingDTO
					{
						Score = rateBarViewModel.Score,
						AppUserId = appUserId.Id,
						AppUserName = this.User.Identity.Name,
						BarId = rateBarViewModel.BarId
					});

					_toastNotification.AddSuccessToastMessage($"You rated this Bar with {rateBarViewModel.Score} stars!");
					return RedirectToAction(nameof(Details), new {id = rateBarViewModel.BarId });
				}
				catch (InvalidOperationException)
				{
					_toastNotification.AddErrorToastMessage("You already rated this bar!");
					return RedirectToAction(nameof(Details), new { id = rateBarViewModel.BarId });
				}
				catch
				{
					return NotFound();
				}
			}
			return RedirectToAction(nameof(Details), rateBarViewModel.BarId);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddComment([Bind("Text, BarId")] BarCommentViewModel barCommentViewModel)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var appUserId = await this._appUserServices.GetUserIdAsync(User.Identity.Name);

					await this._commentServices.AddBarCommentAsync(new BarCommentDTO
					{
						BarId = barCommentViewModel.BarId,
						Text = barCommentViewModel.Text,
						UserId = appUserId.Id,
						UserName = this.User.Identity.Name
					});

					_toastNotification.AddSuccessToastMessage("Your comment was successfully added.");
					return RedirectToAction(nameof(Details), new { id = barCommentViewModel.BarId });
				}
				catch
				{
					return NotFound();
				}
			}
			return View(barCommentViewModel);
		}

		public async Task<IActionResult> Delete(Guid id)
		{
			var barDTO = await this._barServices.GetBarAsync(id);

			if (barDTO == null)
			{
				return NotFound();
			}

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

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			try
			{
				await this._barServices.DeleteBar(id);
				_toastNotification.AddSuccessToastMessage("Bar was successfully deleted!}");
				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{
				_toastNotification.AddErrorToastMessage("Oops! Something went wrong!");
				throw;
			}
		}

		private async Task<bool> BarExists(Guid id)
		{
			return await this._barServices.BarExists(id);
		}
	}
}
