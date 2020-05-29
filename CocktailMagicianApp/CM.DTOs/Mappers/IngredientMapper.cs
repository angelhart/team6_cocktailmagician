using CM.DTOs.Mappers.Contracts;
using CM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CM.DTOs.Mappers
{
    public class IngredientMapper : IIngredientMapper
    {
        public IngredientDTO CreateIngredientDTO(Ingredient ingredient)
        {
            return new IngredientDTO
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Cocktails = ingredient.Cocktails
                            .Select(c => CreateCocktailDTO(c))
                            .ToList()
                // TODO: picture
            };
        }

        public CocktailDTO CreateCocktailDTO(CocktailIngredient ingredient)
        {
            return new CocktailDTO
            {
                Id = ingredient.CocktailId,
                Name = ingredient.Cocktail?.Name
            };
        }
    }
}
