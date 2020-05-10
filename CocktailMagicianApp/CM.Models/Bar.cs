using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CM.Models
{
	public class Bar
	{
        public Bar()
        {
            Comments = new List<CocktailComment>();
            Ratings = new List<CocktailRating>();
            Cocktails = new List<BarCocktail>();
        }
        public Guid Id { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(256)]
        public string Name { get; set; }
        public Address Address { get; set; }
        public bool IsUnlisted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<CocktailComment> Comments { get; set; }
        public ICollection<CocktailRating> Ratings { get; set; }
        public ICollection<BarCocktail> Cocktails { get; set; }
        //TODO Image?
    }
}