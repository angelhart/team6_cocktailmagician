using System;
using System.ComponentModel;

namespace CM.Web.Models
{
	public class CreateCityViewModel
	{
		[DisplayName("Country")]
		public Guid CountryId { get; set; }
		[DisplayName("City Name")]
		public string CityName { get; set; }
	}
}
