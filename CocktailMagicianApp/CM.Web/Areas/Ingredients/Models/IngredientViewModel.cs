using CM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Areas.Ingredients.Models
{
    public class IngredientViewModel
    {
        public IngredientViewModel()
        {
            IngredientCocktails = new List<CocktailIngredientViewModel>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public ICollection<CocktailIngredientViewModel> IngredientCocktails { get; set; }
    }
}
