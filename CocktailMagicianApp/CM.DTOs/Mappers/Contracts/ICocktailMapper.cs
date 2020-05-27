using CM.Models;
using System;

namespace CM.DTOs.Mappers.Contracts
{
    public interface ICocktailMapper
    {
        CocktailDTO CreateCocktailDTO(Cocktail cocktail);
        CocktailCommentDTO CreateCocktailCommentDTO(CocktailComment comment);
        CocktailIngredientDTO CreateCocktailIngredientDTO(CocktailIngredient ingredient);
        CocktailRatingDTO CreateCocktailRatingDTO(CocktailRating rating);
        CocktailSearchDTO CreateCocktailSearchDTO(Cocktail cocktail);
    }
}