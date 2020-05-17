using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CM.DTOs;

namespace CM.Services.Contracts
{
    public interface IAddressServices
    {
        public Task<IEnumerable<CountryDTO>> GetAllCountriesAsync();
		public Task<CityDTO> GetCityAsync(string cityName);
		public Task<CountryDTO> GetCountryAsync(string countryName);
		public Task<ICollection<CityDTO>> GetCountryCitiesAsync(Guid countryId);
        
    }
}
