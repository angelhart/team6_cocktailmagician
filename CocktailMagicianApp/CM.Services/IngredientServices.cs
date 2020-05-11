using CM.DTOs;
using CM.Models;
using CM.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services
{
    public class IngredientServices : IIngredientServices
    {
        public async Task<ICollection<CocktailIngredientDTO>> AddIngredientsToCocktail(Guid cocktailId, ICollection<CocktailIngredientDTO> ingredients)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<CocktailIngredientDTO>> UpdateIngredients(Guid cocktailId, ICollection<CocktailIngredientDTO> ingredients)
        {
            throw new NotImplementedException();
        }
    }
}
