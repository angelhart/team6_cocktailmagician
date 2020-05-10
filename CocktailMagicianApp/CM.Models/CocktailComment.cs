using System;
using System.ComponentModel.DataAnnotations;

namespace CM.Models
{
    public class CocktailComment
    {
        public Guid CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(500)]
        public string Text { get; set; }
        public DateTimeOffset CommentedOn { get; set; }
    }
}