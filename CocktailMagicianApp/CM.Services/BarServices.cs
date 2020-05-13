using CM.DTOs;
using CM.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Services
{
    public class BarServices : IBarServices
    {
        public BarDTO AddReview(int id, BarCommentDTO barCommentDTO)
        {
            throw new NotImplementedException();
        }

        public BarDTO CreateBeer(BarDTO barDTO)
        {
            throw new NotImplementedException();
        }

        public BarDTO DeleteBeer(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<BarDTO> GetAllBars()
        {
            throw new NotImplementedException();
        }

        public BarDTO GetBar(int id)
        {
            throw new NotImplementedException();
        }

        public BarDTO RateBeer(int id, BarRatingDTO barRatingDTO)
        {
            throw new NotImplementedException();
        }

        public BarDTO UpdateBeer(int id, BarDTO barDTO)
        {
            throw new NotImplementedException();
        }
    }
}
