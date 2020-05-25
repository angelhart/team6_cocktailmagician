using CM.DTOs;
using CM.Web.Areas.Ingredients.Models;
using CM.Web.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Providers
{
    public class IngredientViewMapper : IIngredientViewMapper
    {
        public IngredientViewModel CreateIngredientViewModel(IngredientDTO dto)
        {
            return new IngredientViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                // TODO: ImagePath = dto. ,
                IngredientCocktails = dto.Cocktails
                                         .Select(ci => CreateCocktailIngredientViewModel(ci))
                                         .ToList()
            };
        }

        public CocktailIngredientViewModel CreateCocktailIngredientViewModel(CocktailIngredientDTO dto)
        {
            return new CocktailIngredientViewModel
            {
                Ammount = dto.Ammount,
                CocktailId = dto.CocktailId,
                Cocktail = dto.CocktailName,
                IngredientId = dto.IngredientId,
                Ingredient = dto.IngredientName,
                Unit = dto.Unit
            };
        }
    }
}
