using CM.Web.Areas.Magician.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Models
{
    public class CocktailViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Recipe { get; set; }
        [DisplayName("Image")]
        public string ImagePath { get; set; }
        public ICollection<IngredientViewModel> Ingredients { get; set; }
    }
}
