using CM.DTOs;
using CM.Models;
using CM.Services.Providers;
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
        Task<PaginatedList<IngredientDTO>> PageIngredientsAsync(string searchString = "", string sortOrder = "", int pageNumber = 1, int pageSize = 10);
        Task<IngredientDTO> GetIngredientDetailsAsync(Guid id);
        Task<IngredientDTO> UpdateIngredientAsync(IngredientDTO dto);
        Task<int> CountAllIngredientsAsync();
        Task<ICollection<IngredientDTO>> GetAllIngredientsAsync();
    }
}
