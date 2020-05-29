using CM.Models;
using CM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Areas.Magician.Models
{
    public class IngredientViewModel
    {
        public IngredientViewModel()
        {
            Cocktails = new List<CocktailViewModel>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public ICollection<CocktailViewModel> Cocktails { get; set; }
    }
}
