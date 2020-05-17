using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Models
{
	public class City
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public Guid CountryId { get; set; }
		public Country Country { get; set; }

		public ICollection<Address> Adresses { get; set; }
	}
}
