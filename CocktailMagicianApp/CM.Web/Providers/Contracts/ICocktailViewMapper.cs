using CM.DTOs;
using CM.Web.Models;

namespace CM.Web.Providers.Contracts
{
    public interface ICocktailViewMapper
    {
        CocktailViewModel CreateCocktailViewModel(CocktailDTO dto);
    }
}