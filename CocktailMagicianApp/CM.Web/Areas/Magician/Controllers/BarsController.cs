using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CM.Services.Contracts;
using CM.Web.Models;
using CM.DTOs.Mappers.Contracts;
using NToastNotify;
using CM.Services.Providers.Contracts;
using Microsoft.AspNetCore.Authorization;
using CM.Web.Providers.Contracts;
using CM.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CM.Web.Areas.Magician.Controllers
{
	[Area("Magician")]
	[Authorize(Roles = "Magician")]

	public class BarsController : Controller
	{
		private readonly IBarServices _barServices;
		private readonly IAddressServices _addressServices;
		private readonly IAddressMapper _addressMapper;
		private readonly IRatingServices _ratingServices;
		private readonly ICommentServices _commentServices;
		private readonly IAppUserServices _appUserServices;
		private readonly IToastNotification _toastNotification;
		private readonly IDateTimeProvider _dateTimeProvider;
		private readonly ICocktailServices _cocktailServices;
		private readonly IBarViewMapper _barViewMapper;

		public BarsController(IAppUserServices appUserServices, IBarViewMapper barViewMapper, IBarServices barServices, IAddressServices addressServices, IAddressMapper addressMapper, IRatingServices ratingServices,
			IToastNotification toastNotification, ICommentServices commentServices, IDateTimeProvider dateTimeProvider, ICocktailServices cocktailServices)
		{
			_barViewMapper = barViewMapper ?? throw new ArgumentNullException(nameof(barViewMapper));
			_barServices = barServices ?? throw new ArgumentNullException(nameof(barServices));
			_addressServices = addressServices ?? throw new ArgumentNullException(nameof(addressServices));
			_addressMapper = addressMapper ?? throw new ArgumentNullException(nameof(addressMapper));
			_ratingServices = ratingServices ?? throw new ArgumentNullException(nameof(ratingServices));
			_commentServices = commentServices ?? throw new ArgumentNullException(nameof(commentServices));
			_appUserServices = appUserServices ?? throw new ArgumentNullException(nameof(appUserServices));
			_toastNotification = toastNotification ?? throw new ArgumentNullException(nameof(toastNotification));
			_dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
			_cocktailServices = cocktailServices ?? throw new ArgumentNullException(nameof(cocktailServices));
		}

		public async Task<IActionResult> Create()
		{
			var collectionOfCountries = await this._addressServices.GetAllCountriesAsync();
			var listOfCountries = collectionOfCountries.ToList();
			ViewBag.listOfCountries = listOfCountries;

			var collectionOfCocktails = await this._cocktailServices.GetAllCocktailsDDLAsync();
			var listOfCocktails = collectionOfCocktails.ToList();
			ViewBag.listOfCocktails = listOfCocktails;

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(BarViewModel barViewModel)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var barDTO = this._barViewMapper.CreateBarDTO(barViewModel);
					if (!string.IsNullOrEmpty(barViewModel.ImagePath))
						barDTO.ImagePath = barViewModel.ImagePath;

					await this._barServices.CreateBarAsync(barDTO);
					_toastNotification.AddSuccessToastMessage($"Bar {barDTO.Name} was successfully created!");
					return RedirectToAction("Index", "Bars", new { area = ""});
				}
				catch (DbUpdateException)
				{
					_toastNotification.AddErrorToastMessage("Bar with the same name already exists in this city!");
					return RedirectToAction(nameof(Create));
				}
				catch (ArgumentNullException)
				{
					_toastNotification.AddErrorToastMessage("Please enter all parameters!");
					return RedirectToAction(nameof(Create));
				}
			}
			_toastNotification.AddErrorToastMessage("Oops! Something went wrong!");
			return RedirectToAction(nameof(Create));
		}

		public async Task<IActionResult> Edit(Guid id)
		{
			try
			{
				var barDTO = await this._barServices.GetBarAsync(id);
				var barViewModel = this._barViewMapper.CreateBarViewModel(barDTO);

				var collectionOfCocktails = await this._cocktailServices.GetAllCocktailsDDLAsync();
				var listOfCocktails = collectionOfCocktails.ToList();

				var selectedCocktails = barDTO.Cocktails.ToList();

				ViewBag.selectListOfCocktails = new SelectList(listOfCocktails, "Id", "Name", barDTO.Cocktails.ToList());

				return View(barViewModel);
			}
			catch (Exception)
			{
				return NotFound();
			};
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(BarViewModel barViewModel)
		{
			var barDTO = this._barViewMapper.CreateBarDTO_simple(barViewModel);

			if (ModelState.IsValid)
			{
				try
				{
					await _barServices.UpdateBarAsync(barViewModel.Id, barDTO);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (await BarExists(barViewModel.Id) == false)
					{
						return NotFound();
					}
				}
				catch (ArgumentException)
				{
					_toastNotification.AddErrorToastMessage("Please enter all parameters!");
					return View(barViewModel);
				}
				catch
				{
					_toastNotification.AddErrorToastMessage("Oops! Something went wrong!");
					return View(barViewModel);
				}

				_toastNotification.AddSuccessToastMessage($"Bar {barDTO.Name} was successfully updated!");
				return RedirectToAction("Index", "Bars", new { area = "" });
			}
			_toastNotification.AddErrorToastMessage("Oops! Something went wrong!");
			return View(barViewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UpdateListing(Guid id)
		{
			try
			{
				var dto = await _barServices.ChangeListingAsync(id);
				var barViewModel = this._barViewMapper.CreateBarIndexViewModel(dto);

				return Ok(barViewModel);
			}
			catch (Exception ex)
			{
				_toastNotification.AddAlertToastMessage(ex.Message);
				return RedirectToAction("Index", "Bars", new { area = "" });
			}
		}
		private async Task<bool> BarExists(Guid id)
		{
			return await this._barServices.BarExists(id);
		}
	}
}