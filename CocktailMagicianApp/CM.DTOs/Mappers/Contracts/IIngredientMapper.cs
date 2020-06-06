using CM.Models;

namespace CM.DTOs.Mappers.Contracts
{
    public interface IIngredientMapper
    {
        CocktailDTO CreateCocktailDTO(CocktailIngredient ingredient);
        IngredientDTO CreateIngredientDTO(Ingredient ingredient);
    }
}