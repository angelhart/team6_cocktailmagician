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
        Task<IngredientDTO> CreateIngredientAsync(IngredientDTO dto);
        Task<IngredientDTO> DeleteIngredientAsync(Guid id);
        Task<ICollection<IngredientDTO>> GetAllIngredientsAsync();
        Task<IngredientDTO> GetIngredientDetailsAsync(Guid id);
        Task<IngredientDTO> UpdateIngredientAsync(IngredientDTO dto);
    }
}
