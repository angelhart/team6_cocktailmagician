using System.Collections.Generic;

namespace CM.Web.Models
{
	public class TopRated
	{
		public List<TopRatedBarsViewModel> TopRatedBars { get; set; }
		public List<TopRatedCocktailsViewModel> TopRatedCocktails { get; set; }
	}
}
