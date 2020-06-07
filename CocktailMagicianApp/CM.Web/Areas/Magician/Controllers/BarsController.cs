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

namespace CM.Web.Areas.Magician.Controllers
{
    [Area("Magician")]
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


        public BarsController(IAppUserServices appUserServices, IBarServices barServices, IAddressServices addressServices, IAddressMapper addressMapper, IRatingServices ratingServices,
            IToastNotification toastNotification, ICommentServices commentServices, IDateTimeProvider dateTimeProvider, ICocktailServices cocktailServices)
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

                        Address = addressDTO,

                        Cocktails = barViewModel.SelectedCocktails.Select(sc => new CocktailDTO { Id = sc }).ToList()
                    };
                    if (!string.IsNullOrEmpty(barViewModel.ImagePath))
                        barDTO.ImagePath = barViewModel.ImagePath;

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