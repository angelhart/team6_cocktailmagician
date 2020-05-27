using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CM.DTOs;

namespace CM.Services.Contracts
{
    public interface ICommentServices
    {
        Task<BarCommentDTO> AddBarCommentAsync(BarCommentDTO newCommentDto);
        Task<CocktailCommentDTO> AddCocktailCommentAsync(CocktailCommentDTO newCommentDto);
        Task<CocktailCommentDTO> DeleteCocktailCommentAsync(Guid commentId);
        Task<CocktailCommentDTO> EditCocktailCommentAsync(Guid commentId, string text);
        Task<CocktailCommentDTO> GetCocktailCommentAsync(Guid commentId);
    }
}
