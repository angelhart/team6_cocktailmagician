using CM.DTOs;
using CM.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services
{
    public class RatingServices : IRatingServices
    {
        public async Task<BarRatingDTO> RateBar(int id, BarRatingDTO barRatingDTO)
        {
            throw new NotImplementedException();
        }
    }
}
