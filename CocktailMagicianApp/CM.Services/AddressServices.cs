﻿using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers.Contracts;
using CM.Models;
using CM.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services
{
    public class AddressServices : IAddressServices
    {
        private readonly CMContext _context;
        private readonly IAddressMapper _addressMapper;

        public AddressServices(CMContext context, IAddressMapper addressMapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _addressMapper = addressMapper ?? throw new ArgumentNullException(nameof(addressMapper));
        }

        /// <summary>
        /// Retrieves a list of all Countries in the database.
        /// </summary>
        /// <returns>ICollection</returns>
        public async Task<ICollection<CountryDTO>> GetAllCountriesAsync()
        {
            var countriesDTO = await _context.Countries
                .Include(country =>country.Cities)
                .Select(country => this._addressMapper
                .CreateCountryDTO(country))
                .ToListAsync();

            return countriesDTO;
        }

        /// <summary>
        /// Retrieves a list of all Cities for a specified Country in the database.
        /// </summary>
        /// <returns>ICollection</returns>
        public ICollection<CityDTO> GetCountryCitiesAsync(Guid countryId)
        {
            var citiesDTO =  _context.Cities
                    .Where(city => city.CountryId == countryId)
                    .Select(city => this._addressMapper
                    .CreateCityDTO(city))
                    .ToList();
            
            return citiesDTO;
        }

        /// <summary>
        /// Retrieves Country by given Id.
        /// </summary>
        /// <returns>Country</returns>
        public async Task<CountryDTO> GetCountryAsync(Guid countryId)
        {
            //TODO Do we need this?
            var country = await _context.Countries.FirstOrDefaultAsync(country => country.Id == countryId);
           
            if (country == null)
                throw new ArgumentException();

            var countryDTO = this._addressMapper.CreateCountryDTO(country);

            return countryDTO;
        }

        /// <summary>
        /// Retrieves City by given Id.
        /// </summary>
        /// <returns>City</returns>
        public async Task<CityDTO> GetCityAsync(Guid cityId)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(city => city.Id == cityId);

            if (city == null)
                throw new ArgumentException();
            
            var cityDTO = this._addressMapper.CreateCityDTO(city);

            return cityDTO;
        }

        /// <summary>
        /// Creates new Country and adds it to the database.
        /// </summary>
        /// <param name="countryDTO">The params needed for the country to be created.</param>
        /// <returns>Returns CountryDTO with the params of the created country.</returns>
        public async Task<CountryDTO> CreateCountryAsync(CountryDTO countryDTO)
        {
            if (countryDTO.Name == null)
                throw new ArgumentNullException("Country name cannot be null!");

            var countryAlredyExists = _context.Countries
                .Any(country => country.Name == countryDTO.Name);
            if (countryAlredyExists)
                throw new DbUpdateException("A country with the same name already exists!");


            var country = new Country { Name = countryDTO.Name };

            await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();

            countryDTO.Id = countryDTO.Id;

            return countryDTO;
        }

        /// <summary>
        /// Creates new City and adds it to the database.
        /// </summary>
        /// <param name="cityDTO">The params needed for the city to be created.</param>
        /// <returns>Returns CityDTO with the params of the created city.</returns>
        public async Task<CityDTO> CreateCityAsync(CityDTO cityDTO, Guid countryId)
        {
            if (cityDTO.Name == null)
                throw new ArgumentNullException("City name cannot be null!");

            if (await _context.Countries.FirstOrDefaultAsync(country => country.Id == countryId) == null)
                throw new ArgumentNullException("Country was not found!");

            if (await _context.Cities.FirstOrDefaultAsync(city => city.Name == cityDTO.Name) != null)
                throw new DbUpdateException("A city with the same name already exists!");

            var city = new City { Name = cityDTO.Name, CountryId = countryId };

            await _context.Cities.AddAsync(city);
            await _context.SaveChangesAsync();

            cityDTO.Id = cityDTO.Id;

            return cityDTO;
        }

        /// <summary>
        /// Creates new Address and adds it to the database.
        /// </summary>
        /// <param name="cityId">The Id of the city where the ne address will be.</param>
        /// <param name="street">The street of the address.</param>
        /// <returns></returns>
        public async Task<AddressDTO> CreateAddressAsync(AddressDTO addressDTO)
        {

            if (await _context.Cities.FirstOrDefaultAsync(city => city.Id == addressDTO.CityId) == null)
                throw new DbUpdateException("City was not found!");

            if (addressDTO.Street == null)
                throw new ArgumentNullException("Please enter street!");

            var address = new Address
            {
                CityId = addressDTO.CityId,
                Street = addressDTO.Street
            };

            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();

            addressDTO.Id = address.Id;

            return addressDTO;
        }
    }
}
