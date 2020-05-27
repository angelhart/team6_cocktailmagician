using System;
using CM.Models;

namespace CM.Web.Models
{
	public class BarCocktailViewModel
	{
		public Guid BarId { get; set; }
		public string Bar { get; set; }

		public Guid CocktailId { get; set; }
		public string Cocktail { get; set; }

	}
}