using System;
using System.Collections.Generic;
using System.Text;
using CM.DTOs;

namespace CM.Services.Contracts
{
    public interface IBarServices
    {
		BarDTO GetBar(int id);
		ICollection<BarDTO> GetAllBars();
		BarDTO CreateBeer(BarDTO barDTO);
		BarDTO UpdateBeer(int id, BarDTO barDTO);
		BarDTO DeleteBeer(int id);
		BarDTO RateBeer(int id, BarRatingDTO barRatingDTO);
		BarDTO AddReview(int id, BarCommentDTO barCommentDTO);

		//TODO sorting
		//TODO paging

		//IEnumerable<BarDTO> GetIndexbars(int pageNumber, int pageSize, out int count);
	}
}
