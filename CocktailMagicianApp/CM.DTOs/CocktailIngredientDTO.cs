using System;

namespace CM.DTOs
{
    public class CocktailIngredientDTO
    {
        public Guid CocktailId { get; set; }
        public Guid IngredientId { get; set; }
        public int Ammount { get; set; }
        public string Unit { get; set; }
    }
}