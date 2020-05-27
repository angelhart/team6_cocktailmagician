using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CM.DTOs;

namespace CM.Services.Contracts
{
    public interface IRatingServices
    {
        Task<CocktailRatingDTO> DeleteCocktailRatingAsync(Guid userId, Guid cocktailId);
        Task<CocktailRatingDTO> EditCocktailRatingAsync(CocktailRatingDTO input);
        Task<CocktailRatingDTO> GetCocktailRatingAsync(Guid userId, Guid cocktailId);
        Task<BarRatingDTO> RateBarAsync(BarRatingDTO barRatingDTO);
        Task<CocktailRatingDTO> RateCocktailAsync(CocktailRatingDTO input);
    }
}
