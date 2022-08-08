using CM.DTOs;
using CM.Web.Areas.BarCrawler.Models;
using CM.Web.Areas.Magician.Models;
using CM.Web.Models;
using CM.Web.Providers.Contracts;
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
                Bars = dto.Bars
                          .Select(b => CreateBarViewModel(b))
                          .ToList(),
                Comments = dto.Comments
                              .Select(c => CreateCommentViewModel(c))
                              .ToList()
            };
        }

        public CocktailViewModel CreateCocktailViewModel_Simple(CocktailDTO cocktailDTO)
        {
            return new CocktailViewModel
            {
                Id = cocktailDTO.Id,
                Name = cocktailDTO.Name,
                ImagePath = cocktailDTO.ImagePath,
                Price = cocktailDTO.Price
            };
        }

        private BarViewModel CreateBarViewModel(BarDTO dto)
        {
            return new BarViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                ImagePath = dto.ImagePath,
            };

        }
        private CommentViewModel CreateCommentViewModel(CocktailCommentDTO dto)
        {
            return new CommentViewModel
            {
                Id = dto.Id,
                EntityId = dto.CocktailId,
                UserId = dto.UserId,
                UserName = dto.UserName,
                Text = dto.Text,
                CommentedOn = dto.CommentedOn
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
