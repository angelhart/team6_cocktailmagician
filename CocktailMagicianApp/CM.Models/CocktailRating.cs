using System;
using System.ComponentModel.DataAnnotations;
using CM.Models.BaseClasses;

namespace CM.Models
{
    public class CocktailRating : Rating
    {
        public Guid CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
    }
}