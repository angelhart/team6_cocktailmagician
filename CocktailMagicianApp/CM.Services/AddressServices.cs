using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers.Contracts;
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
        public async Task<IEnumerable<CountryDTO>> GetAllCountriesAsync()
        {
            var countriesDTO = await _context.Countries
                .Select(country => this._addressMapper
                .CreateCountryDTO(country))
                .ToListAsync();

            return countriesDTO;
        }

        /// <summary>
        /// Retrieves a list of all Cities for a specified Country in the database.
        /// </summary>
        /// <returns>ICollection</returns>
        public async Task<ICollection<CityDTO>> GetCountryCitiesAsync(Guid countryId)
        {
            var citiesDTO = await _context.Cities
                    .Where(city => city.Country.Id == countryId)
                    .Select(city => this._addressMapper
                    .CreateCityDTO(city))
                    .ToListAsync();
            
            return citiesDTO;
        }

        /// <summary>
        /// Retrieves Country by name.
        /// </summary>
        /// <returns>Country</returns>
        public async Task<CountryDTO> GetCountryAsync(string countryName)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(country => country.Name == countryName);

            var countryDTO = this._addressMapper.CreateCountryDTO(country);

            return countryDTO;
        }

        /// <summary>
        /// Retrieves City by name.
        /// </summary>
        /// <returns>City</returns>
        public async Task<CityDTO> GetCityAsync(string cityName)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(city => city.Name == cityName);

            var cityDTO = this._addressMapper.CreateCityDTO(city);

            return cityDTO;
        }
    }
}
