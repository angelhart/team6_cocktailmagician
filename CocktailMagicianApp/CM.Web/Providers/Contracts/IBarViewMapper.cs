using CM.DTOs;
using CM.Web.Models;

namespace CM.Web.Providers.Contracts
{
	public interface IBarViewMapper
	{
		BarDTO CreateBarDTO(BarViewModel barViewModel);
		BarDTO CreateBarDTO_simple(BarViewModel barViewModel);
		BarIndexViewModel CreateBarIndexViewModel(BarDTO barDTO);
        BarMenuViewModel CreateBarMenuViewModel(BarDTO barDTO);
        BarViewModel CreateBarViewModel(BarDTO barDTO);
    }
}
