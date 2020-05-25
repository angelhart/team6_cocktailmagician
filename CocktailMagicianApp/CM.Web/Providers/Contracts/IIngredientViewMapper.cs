using CM.DTOs;
using CM.Web.Areas.Ingredients.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Providers.Contracts
{
    public interface IIngredientViewMapper
    {
        CocktailIngredientViewModel CreateCocktailIngredientViewModel(CocktailIngredientDTO dto);
        IngredientViewModel CreateIngredientViewModel(IngredientDTO dto);
    }
}
