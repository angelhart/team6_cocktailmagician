using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs
{
    public class CocktailDTO
    {
        public CocktailDTO()
        {
            Ingredients = new List<IngredientDTO>();
            Bars = new List<BarDTO>();
            Comments = new List<CocktailCommentDTO>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Recipe{ get; set; }
        public string ImagePath { get; set; }
        public ICollection<IngredientDTO> Ingredients { get; set; }
        public ICollection<BarDTO> Bars { get; set; }
        public ICollection<CocktailCommentDTO> Comments { get; set; }
        public double? AverageRating { get; set; }
        public bool IsUnlisted { get; set; }
        public float Price { get; internal set; }
    }
}
