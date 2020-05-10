using CM.Models;

namespace CM.DTOs.Mappers.Contracts
{
    public interface IIngredientMapper
    {
        IngredientCocktailDTO CreateIngredientCocktailDTO(CocktailIngredient ingredient);
        IngredientDTO CreateIngredientDTO(Ingredient ingredient);
    }
}