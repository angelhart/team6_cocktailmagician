using CM.DTOs;
using CM.Web.Areas.BarCrawler.Models;
using CM.Web.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Providers.ViewModelMappers
{
    public class RatingViewMapper : IRatingViewMapper
    {
        public CocktailRatingDTO CreateCocktailRatingDTO(RatingViewModel model)
        {
            return new CocktailRatingDTO
            {
                CocktailId = model.EntityId,
                AppUserId = model.UserId,
                Score = model.Score,
            };
        }
    }
}
