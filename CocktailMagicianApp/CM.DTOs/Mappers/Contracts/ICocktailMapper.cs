using CM.Models;
using System;

namespace CM.DTOs.Mappers.Contracts
{
    public interface ICocktailMapper
    {
        CocktailDTO CreateCocktailDTO(Cocktail cocktail);
        CocktailCommentDTO CreateCocktailCommentDTO(CocktailComment comment);
        IngredientDTO CreateIngredientDTO(CocktailIngredient ingredient);
        CocktailRatingDTO CreateCocktailRatingDTO(CocktailRating rating);
        CocktailPricesDTO CreateCocktailPricesDTO(Cocktail cocktail);
    }
}