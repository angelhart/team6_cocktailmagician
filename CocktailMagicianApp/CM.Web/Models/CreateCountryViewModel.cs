using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Models
{
	public class CreateCountryViewModel
	{
		[DisplayName("Country Name")]
		public string CountryName { get; set; }
	}
}
