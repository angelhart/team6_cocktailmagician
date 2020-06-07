using CM.DTOs;
using CM.Web.Areas.BarCrawler.Models;

namespace CM.Web.Providers.Contracts
{
    public interface IRatingViewMapper
    {
        CocktailRatingDTO CreateCocktailRatingDTO(RatingViewModel model);
    }
}