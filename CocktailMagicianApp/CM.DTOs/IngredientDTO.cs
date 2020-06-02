using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs
{
    public class IngredientDTO
    {
        public IngredientDTO()
        {
            Cocktails = new List<CocktailDTO>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int Ammount { get; set; }
        public string Unit { get; set; }
        public ICollection<CocktailDTO> Cocktails { get; set; }
    }
}
