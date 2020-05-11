using CM.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Contracts
{
    public interface ICocktailServices
    {
        Task<CocktailDTO> CocktailListingAsync(Guid cocktailId, bool isUnlisted, bool isAdmin = false);
        Task<CocktailDTO> CreateCocktailAsync(CocktailDTO dto);
        Task<CocktailDTO> GetCocktailDetailsAsync(Guid cocktailId, bool isAdmin = false);
        Task<CocktailDTO> UpdateCocktailAsync(CocktailDTO dto, bool isAdmin = false);
    }
}
