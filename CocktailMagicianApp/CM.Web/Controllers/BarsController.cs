using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CM.Services.Contracts;
using CM.Web.Models;
using CM.DTOs;
using CM.DTOs.Mappers.Contracts;
using NToastNotify;
using CM.Services.Providers.Contracts;
using CM.Web.Providers;
using Microsoft.AspNetCore.Mvc.Rendering;
using CM.Web.Providers.Contracts;

namespace CM.Web.Controllers
{
	public class BarsController : Controller
	{
		private const string ROOTSTORAGE = "\\images\\Bars";

		private readonly IBarServices _barServices;
		private readonly IAddressServices _addressServices;
		private readonly IAddressMapper _addressMapper;
		private readonly IRatingServices _ratingServices;
		private readonly ICommentServices _commentServices;
		private readonly IAppUserServices _appUserServices;
		private readonly IToastNotification _toastNotification;
		private readonly IDateTimeProvider _dateTimeProvider;
		private readonly ICocktailServices _cocktailServices;
		private readonly IStorageProvider _storageProvider;



		public BarsController(IAppUserServices appUserServices, IBarServices barServices, IAddressServices addressServices, IAddressMapper addressMapper, IRatingServices ratingServices,
			IToastNotification toastNotification, ICommentServices commentServices, IDateTimeProvider dateTimeProvider, ICocktailServices cocktailServices, IStorageProvider storageProvider)
		{
			_barServices = barServices ?? throw new ArgumentNullException(nameof(barServices));
			_addressServices = addressServices ?? throw new ArgumentNullException(nameof(addressServices));
			_addressMapper = addressMapper ?? throw new ArgumentNullException(nameof(addressMapper));
			_ratingServices = ratingServices ?? throw new ArgumentNullException(nameof(ratingServices));
			_commentServices = commentServices ?? throw new ArgumentNullException(nameof(commentServices));
			_appUserServices = appUserServices ?? throw new ArgumentNullException(nameof(appUserServices));
			_toastNotification = toastNotification ?? throw new ArgumentNullException(nameof(toastNotification));
			_dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
			_cocktailServices = cocktailServices ?? throw new ArgumentNullException(nameof(cocktailServices));
			_storageProvider = storageProvider ?? throw new ArgumentNullException(nameof(storageProvider));
		}

		public async Task<ActionResult> IndexTable()
		{
			try
			{
				var drawString = HttpContext.Request.Form["draw"].FirstOrDefault();
				int draw = drawString != null ? Convert.ToInt32(drawString) : 0;
				var start = Request.Form["start"].FirstOrDefault();
				var length = Request.Form["length"].FirstOrDefault();
				var searchString = Request.Form["search[value]"].FirstOrDefault();
				var sortBy = Request.Form
								["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"]
								.FirstOrDefault();
				var sortOrder = Request.Form["order[0][dir]"].FirstOrDefault(x => x.Equals("desc"));


				int pageSize = length != null ? Convert.ToInt32(length) : 0;
				int pageNumber = start != null ? (1 + ((int)Math.Ceiling(Convert.ToDouble(start) / pageSize))) : 0;
				int recordsTotal = await _barServices.CountAllBarsAsync();
				bool allowUnlisted = true;

				//if (HttpContext.User.IsInRole("Magician"))
				//	allowUnlisted = true;

				var dtos = await _barServices.GetAllBarsAsync(searchString, pageNumber, pageSize, sortBy, sortOrder, allowUnlisted);
				var vms = dtos.Select(x => new BarIndexViewModel
				{
					Id = x.Id,
					Name = x.Name,
					Country = x.Address.CountryName,
					City = x.Address.CityName,
					Street = x.Address.Street,
					AverageRating =x.AverageRating,
					ImagePath = x.ImagePath,
					IsUnlisted = x.IsUnlisted
					
				}).ToList();

				var recordsFiltered = dtos.SourceItems;

				var output = DataTablesProvider<BarIndexViewModel>.CreateResponse(draw, recordsTotal, recordsFiltered, vms);

				return Ok(output);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
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
				Street = x.Address.Street,
				AverageRating = Math.Round((double)x.AverageRating, 2),
				ImagePath = x.ImagePath,
			});
			return View(barsViewModel);
		}

		public async Task<IActionResult> Details(Guid id)
		{
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
					Name = cocktailDTO.Name
				}).ToList()
			};

			return View(barViewModel);
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

					if (barViewModel.Image != null)
					{
						barViewModel.ImagePath = _storageProvider.GenerateRelativePath(ROOTSTORAGE, barViewModel.Image.FileName, barViewModel.Name);
					}

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

						Cocktails = barViewModel.SelectedCocktails.Select(sc => new CocktailDTO { Id = sc }).ToList()
					};

					await this._barServices.CreateBarAsync(barDTO);

					if (barViewModel.Image != null)
						await _storageProvider.StoreImageAsync(barViewModel.ImagePath, barViewModel.Image);

					_toastNotification.AddSuccessToastMessage($"Bar {barDTO.Name} was successfully created!");
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

				var collectionOfCocktails = await this._cocktailServices.GetAllCocktailsDDLAsync();
				var listOfCocktails = collectionOfCocktails.ToList();
				ViewBag.listOfCocktails = listOfCocktails;

				return View(barViewModel);
			}
			catch (Exception ex)
			{
				_toastNotification.AddWarningToastMessage(ex.Message);
				return RedirectToAction(nameof(Index));
			}
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(BarViewModel barViewModel)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var oldImagePath = barViewModel.ImagePath;

					if (barViewModel.Image != null)
					{
						barViewModel.ImagePath = _storageProvider.GenerateRelativePath(ROOTSTORAGE, barViewModel.Image.FileName, barViewModel.Name);
					}

					var barDTO = new BarDTO
					{
						Id = barViewModel.Id,
						Name = barViewModel.Name,
						Phone = barViewModel.Phone,
						Details = barViewModel.Details,
						ImagePath = barViewModel.ImagePath,

						Cocktails = barViewModel.SelectedCocktails.Select(sc => new CocktailDTO { Id = sc }).ToList()
					};

					await _barServices.UpdateBarAsync(barViewModel.Id, barDTO);

					if (barViewModel.Image != null)
					{
						_storageProvider.DeleteImage(oldImagePath);
						await _storageProvider.StoreImageAsync(barViewModel.ImagePath, barViewModel.Image);
					}

					_toastNotification.AddSuccessToastMessage($"Bar {barDTO.Name} was successfully updated!");
					return RedirectToAction(nameof(Index));
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
			}
			foreach (var item in ModelState.Values)
			{
				foreach (var error in item.Errors)
				{
					_toastNotification.AddWarningToastMessage(error.ErrorMessage);
				}
			}
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
					return RedirectToAction(nameof(Details), new { id = rateBarViewModel.BarId });
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
					var appUserId = await this._appUserServices.GetUserIdAsync(HttpContext.User.Identity.Name);

					barCommentViewModel.UserId = appUserId.Id;
					barCommentViewModel.UserName = HttpContext.User.Identity.Name;
					barCommentViewModel.CommentedOn = this._dateTimeProvider.GetDateTimeDateTimeOffset();

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
					throw new ArgumentException();
				}
			}
			return View(barCommentViewModel);
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



//public async Task<IActionResult> Delete(Guid id)
//{
//	var barDTO = await this._barServices.GetBarAsync(id);

//	if (barDTO == null)
//	{
//		return NotFound();
//	}

//	var barViewModel = new BarViewModel
//	{
//		Id = barDTO.Id,
//		Name = barDTO.Name,
//		Phone = barDTO.Phone,
//		Details = barDTO.Details,
//		ImagePath = barDTO.ImagePath,
//	};

//	return View(barViewModel);
//}