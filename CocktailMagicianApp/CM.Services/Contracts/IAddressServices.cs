using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CM.DTOs;

namespace CM.Services.Contracts
{
    public interface IAddressServices
    {
		public Task<CityDTO> CreateCityAsync(CityDTO cityDTO, Guid countryId);
		public Task<CountryDTO> CreateCountryAsync(CountryDTO countryDTO);
		public Task<ICollection<CountryDTO>> GetAllCountriesAsync();
		public Task<CityDTO> GetCityAsync(Guid cityID);
		public Task<CountryDTO> GetCountryAsync(Guid countryID);
		public Task<ICollection<CityDTO>> GetCountryCitiesAsync(Guid countryId);
        
    }
}
