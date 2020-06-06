using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CM.DTOs;
using CM.Services.Providers;

namespace CM.Services.Contracts
{
    public interface IBarServices
    {
		public Task<BarDTO> GetBarAsync(Guid id);
		public Task<BarDTO> CreateBarAsync(BarDTO barDTO);
		public Task<BarDTO> UpdateBarAsync(Guid id, BarDTO barDTO);
		public Task DeleteBar(Guid id);
		Task<BarDTO> AddCocktailToBar(Guid barId, Guid cocktailId);
		Task<PaginatedList<BarDTO>> GetAllBarsAsync(string searchString = "", int pageNumber = 1, int pageSize = 10, string sortBy = "", string sortOrder = "", bool allowUnlisted = false);
		Task<ICollection<BarDTO>> GetTopBarsAsync(int ammount = 3);
		Task<bool> BarExists(Guid id);
		Task<int> CountAllBarsAsync();
	}
}
