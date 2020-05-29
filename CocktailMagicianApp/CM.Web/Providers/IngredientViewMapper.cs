using CM.DTOs;
using CM.Web.Areas.Magician.Models;
using CM.Web.Models;
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
                Cocktails = dto.Cocktails
                                         .Select(ci => CreateCocktailViewModel(ci))
                                         .ToList()
            };
        }

        public CocktailViewModel CreateCocktailViewModel(CocktailDTO dto)
        {
            throw new NotImplementedException();
            //return new CocktailViewModel
            //{
                
            //};
        }
    }
}
