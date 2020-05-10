using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs
{
    public class CocktailDTO
    {
        public CocktailDTO()
        {

        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<CocktailIngredientDTO> Ingredients { get; set; }
        public ICollection<CocktailBarDTO> Bars { get; set; }
        public ICollection<CocktailCommentDTO> Comments { get; set; }
        public double? AverageRating { get; set; }
        public bool IsUnlisted { get; set; }
    }
}
