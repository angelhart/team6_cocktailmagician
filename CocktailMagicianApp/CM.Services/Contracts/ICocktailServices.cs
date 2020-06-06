using CM.DTOs;
using CM.Services.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Contracts
{
    public interface ICocktailServices
    {
        Task<CocktailDTO> ChangeListingAsync(Guid cocktailId, bool isUnlisted);
        Task<CocktailDTO> CreateCocktailAsync(CocktailDTO dto);
        Task<CocktailDTO> GetCocktailDetailsAsync(Guid cocktailId, bool isAdmin = false);
        Task<ICollection<CocktailDTO>> GetTopCocktailsAsync(int ammount = 3);
        Task<PaginatedList<CocktailDTO>> PageCocktailsAsync(string searchString = "", string sortBy = "", string sortOrder = "", int pageNumber = 1, int pageSize = 10, bool allowUnlisted = false);
        Task<CocktailDTO> UpdateCocktailAsync(CocktailDTO dto);
        Task<CocktailDTO> DeleteAsync(Guid id);
        Task<int> CountAllCocktailsAsync(bool countUnlisted = false);
    }
}
