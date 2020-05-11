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
        Task<ICollection<CocktailIngredientDTO>> AddIngredientsToCocktail(Guid cocktailId, ICollection<CocktailIngredientDTO> ingredients);
        Task<ICollection<CocktailIngredientDTO>> UpdateIngredients(Guid cocktailId, ICollection<CocktailIngredientDTO> ingredients);
    }
}
