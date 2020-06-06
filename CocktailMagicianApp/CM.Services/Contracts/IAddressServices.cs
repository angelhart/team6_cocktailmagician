using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CM.DTOs;
using CM.Models;

namespace CM.Services.Contracts
{
    public interface IAddressServices
    {
		Task<AddressDTO> CreateAddressAsync(AddressDTO addressDTO);
		public Task<CityDTO> CreateCityAsync(string cityName, Guid countryId);
		public Task<CountryDTO> CreateCountryAsync(string countryName);
		Task<AddressDTO> GetAddressAsync(Guid addressId);
		public Task<ICollection<CountryDTO>> GetAllCountriesAsync();
		public Task<CityDTO> GetCityAsync(Guid cityID);
		public Task<CountryDTO> GetCountryAsync(Guid countryID);
		public Task<ICollection<CityDTO>> GetCountryCitiesAsync(Guid countryId);
        
    }
}
