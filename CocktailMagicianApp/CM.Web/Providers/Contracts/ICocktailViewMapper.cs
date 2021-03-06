﻿using CM.DTOs;
using CM.Web.Areas.Magician.Models;
using CM.Web.Models;

namespace CM.Web.Providers.Contracts
{
    public interface ICocktailViewMapper
    {
        CocktailViewModel CreateCocktailViewModel(CocktailDTO dto);
        CocktailDTO CreateCocktailDTO(CocktailViewModel model);
        CocktailDTO CreateCocktailDTO(CocktailModifyViewModel model);
        CocktailModifyViewModel CreateCocktailModifyViewModel(CocktailDTO dto);
		CocktailViewModel CreateCocktailViewModel_Simple(CocktailDTO cocktailDTO);
	}
}