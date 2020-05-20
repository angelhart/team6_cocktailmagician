using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CM.Data;
using CM.Services.Contracts;
using CM.Web.Models;
using CM.DTOs;
using CM.Models;
using CM.DTOs.Mappers.Contracts;

namespace CM.Web.Controllers
{
    public class BarsController : Controller
    {
        private readonly IBarServices _barServices;
        private readonly IAddressServices _addressServices;
        private readonly IAddressMapper _addressMapper;

        public BarsController(IBarServices barServices, IAddressServices addressServices, IAddressMapper addressMapper)
        {
            _barServices = barServices ?? throw new ArgumentNullException(nameof(barServices));
            _addressServices = addressServices ?? throw new ArgumentNullException(nameof(addressServices));
            _addressMapper = addressMapper ?? throw new ArgumentNullException(nameof(addressMapper));
        }

        public async Task<IActionResult> Index()
        {
            var bars = await this._barServices.GetAllBarsAsync();
            var barsViewModel = bars.Select(x => new BarIndexViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Country = x.Address.CountryName,
                City = x.Address.City.Name,
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
                City = barDTO.Address.City.Name,
                Street = barDTO.Address.Street,
                Phone = barDTO.Phone,
                Details = barDTO.Details,
                AverageRating = barDTO.AverageRating,
                ImagePath = barDTO.ImagePath,
            };

            return View(barViewModel);
        }

        public async Task<IActionResult> Create()
        {
            var collectionOfCountries = await this._addressServices.GetAllCountriesAsync();

            var listOfCountries = collectionOfCountries.ToList();
            listOfCountries.Insert(0, new CountryDTO { Id = new Guid(),Name = "Select" }) ;

            ViewBag.listOfCountries = listOfCountries;

            return View();
        }


        public async Task<JsonResult> GetCountryCitiesAsync(Guid countryID)
        {
            var collectionOfCities = await this._addressServices.GetCountryCitiesAsync(countryID);

            var listOfCities = collectionOfCities.ToList();
            listOfCities.Insert(0, new CityDTO { Id = new Guid(), Name = "Select" });

            ViewBag.listOfCities = listOfCities;

            return Json(new SelectList(listOfCities, "Id", "Name"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name, City, Street,Phone,ImagePath,Details")] BarViewModel barViewModel)
        {
            if (ModelState.IsValid)
            {
                //TODO View model to dto mapper
                var addressDTO = new AddressDTO
                {
                    City = await this._addressServices.GetCityAsync(barViewModel.City),
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

                var barViewModel =  new BarViewModel
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
                    City = barDTO.Address.City.Name,
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
