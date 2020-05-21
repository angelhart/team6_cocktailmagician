using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs
{
    public class IngredientDTO
    {
        public IngredientDTO()
        {
            Cocktails = new List<CocktailIngredientDTO>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<CocktailIngredientDTO> Cocktails { get; set; }
        // TODO: picture
    }
}
