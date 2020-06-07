using CM.DTOs;
using CM.Web.Areas.BarCrawler.Models;
using CM.Web.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            };
        }
    }
}
