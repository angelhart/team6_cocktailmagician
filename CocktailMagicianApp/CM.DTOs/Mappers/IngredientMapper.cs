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
            var ingredientDTO = new IngredientDTO
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Cocktails = ingredient.Cocktails
                            .Select(c => CreateCocktailDTO(c))
                            .ToList(),
                ImagePath = ingredient.ImagePath
            };

            if (string.IsNullOrEmpty(ingredientDTO.ImagePath))
                ingredientDTO.ImagePath = "/images/DefaultIngredients.png";

            return ingredientDTO;
        }

        public CocktailDTO CreateCocktailDTO(CocktailIngredient ingredient)
        {
            return new CocktailDTO
            {
                Id = ingredient.CocktailId,
                Name = ingredient.Cocktail?.Name,
                ImagePath = ingredient.Cocktail?.ImagePath
            };
        }
    }
}
