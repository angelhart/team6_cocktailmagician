using System;

namespace CM.Web.Models
{
	public class TopRatedCocktailsViewModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string ImagePath { get; set; }
		public double? AverageRating { get; set; }

	}
}