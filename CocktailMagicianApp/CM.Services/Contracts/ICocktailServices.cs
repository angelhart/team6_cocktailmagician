using CM.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Contracts
{
    public interface ICocktailServices
    {
        Task<CocktailDTO> CocktailListing(Guid cocktailId, bool isUnlisted, bool isAdmin = false);
        Task<CocktailDTO> CreateCocktail(CocktailDTO dto);
        Task<CocktailDTO> GetCocktailDetails(Guid cocktailId, bool isAdmin = false);
        Task<CocktailDTO> UpdateCocktail(Guid cocktailId, CocktailDTO dto, bool isAdmin = false);
    }
}
