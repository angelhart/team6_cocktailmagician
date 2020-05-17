using System;

namespace CM.DTOs
{
    public class CocktailIngredientDTO
    {
        public Guid IngredientId { get; set; }
        public string IngredientName { get; set; }
        public Guid CocktailId { get; set; }
        public string CocktailName { get; set; }
        public int Ammount { get; set; }
        public string Unit { get; set; }
    }
}