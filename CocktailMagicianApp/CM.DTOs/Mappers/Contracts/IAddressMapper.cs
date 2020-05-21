using System;
using System.Collections.Generic;
using System.Text;
using CM.Models;

namespace CM.DTOs.Mappers.Contracts
{
	public interface IAddressMapper
	{
		public CountryDTO CreateCountryDTO(Country country);
		public CityDTO CreateCityDTO(City city);
		AddressDTO CreateAddressDTO(Address address);
		Address CreateAddress(BarDTO barDTO);
	}
}
