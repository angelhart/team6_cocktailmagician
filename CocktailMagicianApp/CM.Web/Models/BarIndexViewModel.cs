using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CM.Models;

namespace CM.Web.Models
{
	public class BarIndexViewModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public Guid CountryID { get; set; }
		public string Country { get; set; }
		public Guid CityID { get; set; }
		public string City { get; set; }
		public double? AverageRating { get; set; }
		public string ImagePath { get; set; }


	}
}
