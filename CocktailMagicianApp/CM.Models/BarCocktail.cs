using System;

namespace CM.Models
{
    public class BarCocktail
    {
        public Guid BarId { get; set; }
        public Bar Bar { get; set; }

        public Guid CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
    }
}