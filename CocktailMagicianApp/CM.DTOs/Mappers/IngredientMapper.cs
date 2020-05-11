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
                            .Select(c => CreateIngredientCocktailDTO(c))
                            .ToList()
                // TODO: picture
            };
        }

        public IngredientCocktailDTO CreateIngredientCocktailDTO(CocktailIngredient ingredient)
        {
            return new IngredientCocktailDTO
            {
                CocktailId = ingredient.CocktailId,
                CocktailName = ingredient.Cocktail?.Name
            };
        }

        public Ingredient CreateIngredient(IngredientDTO dto)
        {
            return new Ingredient
            {
                Name = dto.Name
                // TODO: picture
            };
        }
    }
}
