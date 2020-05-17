using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CM.DTOs.Mappers.Contracts;
using CM.Models;

namespace CM.DTOs.Mappers
{
	public class AddressMapper : IAddressMapper
	{
		public CityDTO CreateCityDTO(City city)
		{
			return new CityDTO
			{
				Id = city.Id,
				Name = city.Name,
				//CoutryId = city.CountryId,
				//Country = CreateCountryDTO(city.Country),
			};
		}

		public CountryDTO CreateCountryDTO(Country country)
		{
			return new CountryDTO
			{
				Id = country.Id,
				Name = country.Name,
				Cities = country.Cities.Select(city => CreateCityDTO(city)).ToList()
			};
		}

		public AddressDTO CreateAddressDTO(Address address)
		{
			return new AddressDTO
			{
				Id = address.Id,
				City = CreateCityDTO(address.City),
				Street = address.Street,
				CountryName = address.City.Country.Name
			};
		}
	}
}
