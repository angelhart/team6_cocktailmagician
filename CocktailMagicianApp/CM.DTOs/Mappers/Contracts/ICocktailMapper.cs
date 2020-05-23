﻿using CM.Models;
using System;

namespace CM.DTOs.Mappers.Contracts
{
    public interface ICocktailMapper
    {
        CocktailDTO CreateCocktailDTO(Cocktail cocktail);
        CocktailCommentDTO CreateCocktailCommentDTO(CocktailComment comment);
        CocktailIngredientDTO CreateCocktailIngredientDTO(CocktailIngredient ingredient);
        CocktailRatingDTO CreateCocktailRatingDTO(CocktailRating rating);
        Cocktail CreateCocktail(CocktailDTO dto);
        CocktailSearchDTO CreateCocktailSearchDTO(Cocktail cocktail);
        CocktailIngredient CreateCocktailIngredient(Guid cocktailId, CocktailIngredientDTO dto);
        CocktailComment CreateCocktailComment(CocktailCommentDTO comment);
        CocktailRating CreateCocktailRating(CocktailRatingDTO input);
    }
}