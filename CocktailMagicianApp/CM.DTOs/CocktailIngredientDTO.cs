using System;

namespace CM.DTOs
{
    public class CocktailIngredientDTO
    {
        public Guid IngredientId { get; set; }
        public string Name { get; set; }
        public int Ammount { get; set; }
        public string Unit { get; set; }
    }
}