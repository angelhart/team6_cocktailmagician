using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Areas.Ingredients.Models
{
    public class CocktailIngredientViewModel
    {
        public Guid IngredientId { get; set; }
        public string Ingredient { get; set; }
        public int Ammount { get; set; }
        public string Unit { get; set; }
        public Guid CocktailId { get; set; }
        public string Cocktail { get; internal set; }
    }
}
