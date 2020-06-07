using System;

namespace CM.DTOs
{
	public class AddressDTO
	{
        public Guid Id { get; set; }
        public Guid BarId { get; set; }
        public Guid CityId { get; set; }

        public CityDTO City { get; set; }

        public string CountryName { get; set; }

        public string CityName { get; set; }

        public string Street { get; set; }
    }
}