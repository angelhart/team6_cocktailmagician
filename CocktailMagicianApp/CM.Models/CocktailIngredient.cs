using System;
using System.ComponentModel.DataAnnotations;

namespace CM.Models
{
    public class CocktailIngredient
    {
        public Guid CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
        public Guid IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        [Required]
        public int Ammount { get; set; }
        [Required]
        public Unit Unit{ get; set; }
    }
}