using CM.DTOs;
using CM.Web.Areas.BarCrawler.Models;

namespace CM.Web.Providers.Contracts
{
    public interface ICommentViewMapper
    {
		BarCommentDTO CreateBarCommentDTO(CommentViewModel model);
		CommentViewModel CreateBarCommentViewModel(BarCommentDTO barDTO);
		CocktailCommentDTO CreateCocktailCommentDTO(CommentViewModel model);
    }
}