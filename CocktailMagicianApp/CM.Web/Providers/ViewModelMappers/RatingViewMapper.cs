using CM.DTOs;
using CM.Web.Areas.BarCrawler.Models;
using CM.Web.Providers.Contracts;

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

        public BarRatingDTO CreateBarRatingDTO(RatingViewModel rateBarViewModel)
        {
            return new BarRatingDTO
            {
                Score = rateBarViewModel.Score,
                AppUserId = rateBarViewModel.UserId,
                AppUserName = rateBarViewModel.UserName,
                BarId = rateBarViewModel.EntityId
            };
        }
    }
}
