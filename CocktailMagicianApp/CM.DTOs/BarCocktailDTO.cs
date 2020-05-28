using System;
using CM.Models;

namespace CM.DTOs
{
	public class BarCocktailDTO
	{
		//public Guid Id { get; set; }
		//public string Name { get; set; }
		public Guid BarId { get; set; }
		public string Bar { get; set; }

		public Guid CocktailId { get; set; }
		public Cocktail Cocktail { get; set; }
	}
}