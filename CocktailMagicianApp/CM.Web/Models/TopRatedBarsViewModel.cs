using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Models
{
	public class TopRatedBarsViewModel
	{
		public string Name { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public string ImagePath { get; set; }
		public double? AverageRating { get; set; }
	}
}
