using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
            Bars = new List<BarCocktail>();
        }
        public Guid Id { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(256)]
        public string Name { get; set; }
        public string Recipe { get; set; }
        public bool IsUnlisted { get; set; }
        public ICollection<CocktailIngredient> Ingredients { get; set; }
        public ICollection<CocktailComment> Comments { get; set; }
        public ICollection<CocktailRating> Ratings { get; set; }
        // AverageRating ignored in configuration
        public double? AverageRating { get; set; } // => Ratings.Average(r => r.Score);
        public ICollection<BarCocktail> Bars { get; set; }
        // TODO: picture
    }
}
