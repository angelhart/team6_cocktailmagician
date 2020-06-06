using CM.DTOs;
using CM.Web.Areas.Magician.Models;
using CM.Web.Models;
using CM.Web.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Providers.ViewModelMappers
{
    public class IngredientViewMapper : IIngredientViewMapper
    {
        public IngredientViewModel CreateIngredientViewModel(IngredientDTO dto)
        {
            return new IngredientViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                ImagePath = dto.ImagePath,
                Cocktails = dto.Cocktails
                                         .Select(ci => CreateCocktailViewModel(ci))
                                         .ToList()
            };
        }

        public CocktailViewModel CreateCocktailViewModel(CocktailDTO dto)
        {
            return new CocktailViewModel
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }

        public IngredientDTO CreateIngredientDTO(IngredientViewModel model)
        {
            return new IngredientDTO
            {
                Id = model.Id,
                Name = model.Name,
                ImagePath = model.ImagePath,
                Cocktails = model.Cocktails
                                 .Select(ci => CreateCocktailDto(ci))
                                 .ToList()
            };
        }

        private CocktailDTO CreateCocktailDto(CocktailViewModel ci)
        {
            throw new NotImplementedException("CreateCocktailDto");
        }
    }
}
