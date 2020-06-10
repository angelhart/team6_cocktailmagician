using CM.DTOs;
using CM.Web.Areas.BarCrawler.Models;

namespace CM.Web.Providers.Contracts
{
    public interface IRatingViewMapper
    {
		BarRatingDTO CreateBarRatingDTO(RatingViewModel rateBarViewModel);
		CocktailRatingDTO CreateCocktailRatingDTO(RatingViewModel model);
    }
}