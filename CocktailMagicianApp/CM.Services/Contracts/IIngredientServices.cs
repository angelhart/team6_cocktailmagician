using CM.DTOs;
using CM.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Contracts
{
    public interface IIngredientServices
    {
        Task<ICollection<CocktailIngredientDTO>> AddIngredientsToCocktailAsync(Guid cocktailId, ICollection<CocktailIngredientDTO> ingredients);
        Task<IngredientDTO> CreateIngredientAsync(IngredientDTO dto);
        Task<IngredientDTO> GetIngredientDetailsAsync(Guid id);
        Task<ICollection<CocktailIngredientDTO>> UpdateCocktailIngredientsAsync(Guid cocktailId, ICollection<CocktailIngredientDTO> ingredients);
    }
}
