using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CM.DTOs;

namespace CM.Services.Contracts
{
    public interface IBarServices
    {
		public  Task<BarDTO> GetBar(Guid id);
		public  Task<ICollection<BarDTO>> GetAllBars();
		public  Task<BarDTO> CreateBar(BarDTO barDTO);
		public  Task<BarDTO> UpdateBar(Guid id, BarDTO barDTO);

		//TODO sorting
		//TODO paging

		//IEnumerable<BarDTO> GetIndexbars(int pageNumber, int pageSize, out int count);
	}
}
