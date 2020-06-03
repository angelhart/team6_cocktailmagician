﻿using System;
using CM.Models;

namespace CM.DTOs
{
	public class AddressDTO
	{
        public Guid Id { get; set; }
        public Guid BarId { get; set; }
        public Guid CityId { get; set; }
        public string CountryName { get; set; }

        public string CityName { get; set; }

        public string Street { get; set; }
    }
}