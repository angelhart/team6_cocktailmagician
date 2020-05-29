using CM.DTOs;
using CM.Web.Areas.Magician.Models;
using CM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Providers.Contracts
{
    public interface IIngredientViewMapper
    {
        CocktailViewModel CreateCocktailViewModel(CocktailDTO dto);
        IngredientViewModel CreateIngredientViewModel(IngredientDTO dto);
    }
}
