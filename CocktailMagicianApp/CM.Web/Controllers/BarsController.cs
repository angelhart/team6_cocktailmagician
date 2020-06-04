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
using Microsoft.CodeAnalysis.CSharp.Syntax;
using CM.Services.Providers.Contracts;

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
		private readonly IDateTimeProvider _dateTimeProvider;
		private readonly ICocktailServices _cocktailServices;


		public BarsController(IAppUserServices appUserServices,IBarServices barServices, IAddressServices addressServices, IAddressMapper addressMapper, IRatingServices ratingServices,
			IToastNotification toastNotification, ICommentServices commentServices, IDateTimeProvider dateTimeProvider,ICocktailServices cocktailServices)
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
		}

		//public async Task<ActionResult> IndexTable()
		//{
		//	try
		//	{
		//		var drawString = HttpContext.Request.Form["draw"].FirstOrDefault();
		//		int draw = drawString != null ? Convert.ToInt32(drawString) : 0;
		//		var start = Request.Form["start"].FirstOrDefault();
		//		var length = Request.Form["length"].FirstOrDefault();
		//		var searchString = Request.Form["search[value]"].FirstOrDefault();
		//		var sortBy = Request.Form
		//						["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"]
		//						.FirstOrDefault();
		//		var sortOrder = Request.Form["order[0][dir]"].FirstOrDefault(x => x.Equals("desc"));

		//		int pageSize = length != null ? Convert.ToInt32(length) : 0;
		//		int pageNumber = start != null ? (1 + ((int)Math.Ceiling(Convert.ToDouble(start) / pageSize))) : 0;
		//		int recordsTotal = await _ingredientServices.CountAllIngredientsAsync();

		//		var dtos = await _ingredientServices.PageIngredientsAsync(searchString, pageNumber, pageSize);
		//		var vms = dtos.Select(d => _ingredientViewMapper.CreateIngredientViewModel(d)).ToList();

		//		var recordsFiltered = dtos.SourceItems;

		//		var output = DataTablesProvider<IngredientViewModel>.CreateResponse(draw, recordsTotal, recordsFiltered, vms);

		//		return Ok(output);
		//	}
		//	catch (Exception e)
		//	{
		//		return BadRequest(e.Message);
		//	}
		//}
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
					Name = cocktailDTO.Name
				}).ToList()
			};

			return View(barViewModel);
		}

		public async Task<IActionResult> Create()
		{
			var collectionOfCountries = await  this._addressServices.GetAllCountriesAsync();
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

						Cocktails =barViewModel.SelectedCocktails.Select(sc => new CocktailDTO { Id = sc }).ToList()
					};

					await this._barServices.CreateBarAsync(barDTO);
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
