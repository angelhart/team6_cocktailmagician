using CM.Models;

namespace CM.DTOs.Mappers.Contracts
{
    public interface IIngredientMapper
    {
        CocktailIngredientDTO CreateCocktailIngredientDTO(CocktailIngredient ingredient);
        IngredientDTO CreateIngredientDTO(Ingredient ingredient);
    }
}