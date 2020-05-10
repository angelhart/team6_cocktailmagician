using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Models
{
	public class Address
	{
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public Guid BarId { get; set; }
        public Bar Bar { get; set; }
    }
}
