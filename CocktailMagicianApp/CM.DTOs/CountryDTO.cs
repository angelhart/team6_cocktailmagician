using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs
{
	public class CountryDTO
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public ICollection<CityDTO> Cities { get; set; }
	}
}
