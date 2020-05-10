using System;
using System.ComponentModel.DataAnnotations;

namespace CM.Models
{
    public class CocktailRating
    {
        public Guid CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        [Required]
        [Range(1,5)]
        public int Score { get; set; }
    }
}