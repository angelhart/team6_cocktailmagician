using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CM.DTOs;

namespace CM.Services.Contracts
{
    public interface IBarServices
    {
		public Task<BarDTO> GetBarAsync(Guid id);
		public Task<ICollection<BarDTO>> GetAllBarsAsync();
		public Task<BarDTO> CreateBarAsync(BarDTO barDTO);
		public Task<BarDTO> UpdateBarAsync(Guid id, BarDTO barDTO);
		public Task<BarDTO> DeleteBar(Guid id);
		Task<BarDTO> AddCocktailToBar(Guid barId, Guid cocktailId);

		//TODO sorting
		//TODO paging
	}
}
