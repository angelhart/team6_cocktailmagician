﻿using CM.DTOs.Mappers.Contracts;
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
            return new CocktailDTO
            {
                Id = cocktail.Id,
                Name = cocktail.Name,
                Recipe = cocktail.Recipe,
                IsUnlisted = cocktail.IsUnlisted,
                AverageRating = cocktail.AverageRating,
                Bars = cocktail.Bars
                        .Select(b => CreateCocktailBarDTO(b))
                        .ToList(),
                Comments = cocktail.Comments
                        .Select(c => CreateCocktailCommentDTO(c))
                        .ToList(),
                Ingredients = cocktail.Ingredients
                        .Select(i => CreateCocktailIngredientDTO(i))
                        .ToList()
            };
        }

        public CocktailSearchDTO CreateCocktailSearchDTO(Cocktail cocktail)
        {
            return new CocktailSearchDTO
            {
                Id = cocktail.Id,
                Name = cocktail.Name,
                Ingredients = cocktail.Ingredients
                                .Select(i => i.Ingredient.Name)
                                .ToList()
            };
        }

        private BarCocktailDTO CreateCocktailBarDTO(BarCocktail bar)
        {
            return new BarCocktailDTO
            {
                BarId = bar.BarId,
                Bar = bar.Bar?.Name
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

        public CocktailIngredientDTO CreateCocktailIngredientDTO(CocktailIngredient ingredient)
        {
            return new CocktailIngredientDTO
            {
                IngredientId = ingredient.IngredientId,
                IngredientName = ingredient.Ingredient?.Name,
                Ammount = ingredient.Ammount,
                Unit = ingredient.Unit.ToString()
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
    }
}