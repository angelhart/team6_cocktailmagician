using CM.DTOs;
using CM.Web.Areas.BarCrawler.Models;
using CM.Web.Providers.Contracts;

namespace CM.Web.Providers.ViewModelMappers
{
    public class CommentViewMapper : ICommentViewMapper
    {
        public CocktailCommentDTO CreateCocktailCommentDTO(CommentViewModel model)
        {
            return new CocktailCommentDTO
            {
                CocktailId = model.EntityId,
                UserId = model.UserId,
                Text = model.Text,
                CommentedOn = model.CommentedOn
            };
        }

        public BarCommentDTO CreateBarCommentDTO(CommentViewModel model)
        {
            return new BarCommentDTO
            {
                BarId = model.EntityId,
                Text = model.Text,
                UserId = model.UserId,
                CommentedOn = model.CommentedOn,
                UserName = model.UserName
            };
        }

        public CommentViewModel CreateBarCommentViewModel(BarCommentDTO barDTO)
        {
            return new CommentViewModel
            {
                Text = barDTO.Text,
                UserName = barDTO.UserName,
                CommentedOn = barDTO.CommentedOn
            };
        }

    }
}
