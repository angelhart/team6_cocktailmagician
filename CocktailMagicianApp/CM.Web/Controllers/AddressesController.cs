using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CM.Services.Contracts;
using NToastNotify;

namespace CM.Web.Controllers
{
    public class AddressesController : Controller
    {
        private readonly IAddressServices _addressServices;
        private readonly IToastNotification _toastNotification;

        public AddressesController(IToastNotification toastNotification, IBarServices barServices, IAddressServices addressServices)
        {
            _addressServices = addressServices ?? throw new ArgumentNullException(nameof(addressServices));
            _toastNotification = toastNotification ?? throw new ArgumentNullException(nameof(toastNotification));
        }

        public async Task<IActionResult> CreateCountry()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCountry(string countryName)
        {
			try
			{
                await this._addressServices.CreateCountryAsync(countryName);
                _toastNotification.AddSuccessToastMessage($"Country {countryName} was successfully created!");
                return RedirectToAction(nameof(CreateCity));
            }
            catch (DbUpdateException)
			{
                _toastNotification.AddErrorToastMessage("A country with the same name already exists!");
                return View(nameof(CreateCountry));
            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Oops! Something went wrong!");
                return View(nameof(CreateCountry));
            }
        }

        public async Task<IActionResult> CreateCity()
        {
            var collectionOfCountries = await this._addressServices.GetAllCountriesAsync();

            var listOfCountries = collectionOfCountries.ToList();

            ViewBag.listOfCountries = listOfCountries;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCity(Guid countryId, string cityName)
        {
            try
            {
                await this._addressServices.CreateCityAsync(cityName, countryId);
                _toastNotification.AddSuccessToastMessage($"City {cityName} was successfully created!");
                return RedirectToAction("Create", "Bars", new { area = "" });
            }
            catch (DbUpdateException)
            {
                _toastNotification.AddErrorToastMessage("A city with the same name already exists!");
                return View(nameof(CreateCountry));
            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Oops! Something went wrong!");
                return View(nameof(CreateCountry));
            }
        }


        public async Task<IActionResult> GetCountryCities(Guid countryId)
        {
            var collectionOfCities = await this._addressServices.GetCountryCitiesAsync(countryId);

            var listOfCities = collectionOfCities.ToList();

            return Json(listOfCities);
        }


        //// POST: Addresses/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, Address address)
        //{
        //    if (id != address.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(address);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!AddressExists(address.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["BarId"] = new SelectList(_context.Bars, "Id", "Name", address.BarId);
        //    ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Id", address.CityId);
        //    return View(address);
        //}

        ////// GET: Addresses/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var address = await _context.Addresses
        //        .Include(a => a.Bar)
        //        .Include(a => a.City)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (address == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(address);
        //}

        //// POST: Addresses/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var address = await _context.Addresses.FindAsync(id);
        //    _context.Addresses.Remove(address);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool AddressExists(Guid id)
        //{
        //    return _context.Addresses.Any(e => e.Id == id);
        //}
        //public async Task<IActionResult> ChangeAddress(Guid barId)
        //{

        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ChangeAddress(BarViewModel barViewModel)
        //{
        //    if (id != barViewModel.Id)
        //    {
        //        return NotFound();
        //    }

        //    var barDTO = new BarDTO
        //    {
        //        Id = barViewModel.Id,
        //        Name = barViewModel.Name,
        //    };

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            await _barServices.UpdateBarAsync(id, barDTO);
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //                _toastNotification.AddErrorToastMessage("Oops! Something went wrong!");
        //                throw;
        //        }
        //        _toastNotification.AddSuccessToastMessage($"The address of Bar {barDTO.Name} was successfully updated!");
        //        return RedirectToAction(nameof(Index));
        //    }
        //    _toastNotification.AddErrorToastMessage("Oops! Something went wrong!");
        //    return View(barViewModel);
        //}
        //public async Task<IActionResult> Edit(Guid id)
        //{
        //    try
        //    {
        //        var addressDTO = await this._addressServices.GetAddressAsync(id);

        //        ViewData["Country"] = new SelectList(await this._addressServices.GetAllCountriesAsync(), "Id", "Name");
        //        //ViewData["City"] = new SelectList(await this._addressServices.GetCountryCities(ViewBag.Country), "Id", "Name");

        //        var barViewModel = new BarViewModel
        //        {
        //            Country = addressDTO.CountryName,
        //            City = addressDTO.CityName,
        //            Street = addressDTO.Street
        //        };

        //        return View(barViewModel);
        //    }
        //    catch (Exception)
        //    {
        //        return NotFound();
        //    };
        //}

    }
}
