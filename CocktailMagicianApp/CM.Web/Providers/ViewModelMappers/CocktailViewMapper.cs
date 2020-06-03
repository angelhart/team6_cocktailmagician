using CM.DTOs;
using CM.Web.Areas.Magician.Models;
using CM.Web.Models;
using CM.Web.Providers.Contracts;
using System;
using System.Linq;

namespace CM.Web.Providers.ViewModelMappers
{
    public class CocktailViewMapper : ICocktailViewMapper
    {
        public CocktailDTO CreateCocktailDTO(CocktailViewModel model)
        {
            return new CocktailDTO
            {
                Id = model.Id,
                Name = model.Name,
                ImagePath = model.ImagePath,
                Recipe = model.Recipe,
                IsUnlisted = model.IsUnlisted,
                Ingredients = model.Ingredients
                                   .Select(i => CreateIngredientDto(i))
                                   .ToList(),
            };
        }

        public CocktailDTO CreateCocktailDTO(CocktailModifyViewModel model)
        {
            return new CocktailDTO
            {
                Id = model.Id,
                Name = model.Name,
                ImagePath = model.ImagePath,
                Recipe = model.Recipe,
                IsUnlisted = model.IsUnlisted,
                Ingredients = model.Ingredients
                                   .Select(i => new IngredientDTO { Id = i})
                                   .ToList(),
            };
        }

        private IngredientDTO CreateIngredientDto(IngredientViewModel model)
        {
            return new IngredientDTO
            {
                Id = model.Id,
                Name = model.Name,
                ImagePath = model.ImagePath,
            };
        }

        public CocktailViewModel CreateCocktailViewModel(CocktailDTO dto)
        {
            return new CocktailViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Recipe = dto.Recipe,
                AverageRating = dto.AverageRating,
                ImagePath = dto.ImagePath,
                IsUnlisted = dto.IsUnlisted,
                Ingredients = dto.Ingredients
                                 .Select(i => CreateIngredientViewModel(i))
                                 .ToList(),
            };
        }

        public CocktailModifyViewModel CreateCocktailModifyViewModel(CocktailDTO dto)
        {
            return new CocktailModifyViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Recipe = dto.Recipe,
                ImagePath = dto.ImagePath,
                IsUnlisted = dto.IsUnlisted,
                Ingredients = dto.Ingredients
                                 .Select(i => i.Id)
                                 .ToList(),
            };
        }

        private IngredientViewModel CreateIngredientViewModel(IngredientDTO dto)
        {
            return new IngredientViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                ImagePath = dto.ImagePath
            };
        }
    }
}
