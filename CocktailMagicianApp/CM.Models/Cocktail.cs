using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CM.Models
{
    public class Cocktail
    {
        public Cocktail()
        {
            Ingredients = new List<CocktailIngredient>();
            Comments = new List<CocktailComment>();
            Ratings = new List<CocktailRating>();
            Bars = new List<BarCocktails>();
        }
        public Guid Id { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(256)]
        public string Name { get; set; }
        public bool IsUnlisted { get; set; }
        public ICollection<CocktailIngredient> Ingredients { get; set; }
        public ICollection<CocktailComment> Comments { get; set; }
        public ICollection<CocktailRating> Ratings { get; set; }
        public ICollection<BarCocktails> Bars { get; set; }
    }
}
