using CM.DTOs;
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
        Task<CocktailDTO> UpdateCocktailAsync(CocktailDTO dto);
    }
}
