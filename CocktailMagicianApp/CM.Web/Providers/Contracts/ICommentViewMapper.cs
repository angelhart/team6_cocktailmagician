using CM.DTOs;
using CM.Web.Areas.BarCrawler.Models;

namespace CM.Web.Providers.Contracts
{
    public interface ICommentViewMapper
    {
        CocktailCommentDTO CreateCocktailCommentDTO(CommentViewModel model);
    }
}