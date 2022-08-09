using CM.DTOs.Mappers.Contracts;
using CM.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CM.DTOs.Mappers
{
    public class CocktailMapper : ICocktailMapper
    {
        public CocktailDTO CreateCocktailDTO(Cocktail cocktail)
        {
            var cocktailDTO = new CocktailDTO
            {
                Id = cocktail.Id,
                Name = cocktail.Name,
                Recipe = cocktail.Recipe,
                IsUnlisted = cocktail.IsUnlisted,
                AverageRating = cocktail.AverageRating,
                ImagePath = cocktail.ImagePath,
                Bars = cocktail.Bars
                        .Select(b => CreateBarDTO(b))
                        .ToList(),
                Comments = cocktail.Comments
                        .Select(c => CreateCocktailCommentDTO(c))
                        .OrderByDescending(comment => comment.CommentedOn)
                        .ToList(),
                Ingredients = cocktail.Ingredients
                        .Select(i => CreateIngredientDTO(i))
                        .ToList()
            };

            if (string.IsNullOrEmpty(cocktailDTO.ImagePath))
                cocktailDTO.ImagePath = "/images/DefaultCocktail.png";

            return cocktailDTO;
        }

        private BarDTO CreateBarDTO(BarCocktail bar)
        {
            return new BarDTO
            {
                Id = bar.BarId,
                Name = bar.Bar?.Name,
                ImagePath = bar.Bar?.ImagePath,
            };
        }

        public CocktailCommentDTO CreateCocktailCommentDTO(CocktailComment comment)
        {
            return new CocktailCommentDTO
            {
                Id = comment.Id,
                UserId = comment.AppUserId,
                UserName = comment.AppUser?.UserName,
                CocktailId = comment.CocktailId,
                //CocktailName = comment.Cocktail?.Name,
                CommentedOn = comment.CommentedOn,
                Text = comment.Text
            };
        }

        public IngredientDTO CreateIngredientDTO(CocktailIngredient ingredient)
        {
            return new IngredientDTO
            {
                Id = ingredient.IngredientId,
                Name = ingredient.Ingredient?.Name,
                ImagePath = ingredient.Ingredient?.ImagePath,
                //Ammount = ingredient.Ammount,
                //Unit = ingredient.Unit.ToString()
            };
        }

        public CocktailRatingDTO CreateCocktailRatingDTO(CocktailRating rating)
        {
            return new CocktailRatingDTO
            {
                AppUserId = rating.AppUserId,
                CocktailId = rating.CocktailId,
                Score = rating.Score
            };
        }

        public CocktailPricesDTO CreateCocktailPricesDTO(Cocktail cocktail)
        {
            return new CocktailPricesDTO
            {
                Id = cocktail.Id,
                Name = cocktail.Name,
                ImagePath = cocktail.ImagePath,
                BarPrices = cocktail.Bars.Select(bc => CreateBarPriceDTO(bc))
                                         .ToList()
            };
        }

        public BarPriceDTO CreateBarPriceDTO(BarCocktail barCocktail)
        {
            return new BarPriceDTO
            {
                Id = barCocktail.BarId,
                Name = barCocktail.Bar.Name,
                ImagePath = barCocktail.Bar.ImagePath,
                Price = barCocktail.Price,
                IsUnlisted = barCocktail.Bar.IsUnlisted
            };
        }
    }
}