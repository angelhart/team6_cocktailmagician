using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs
{
    public class CocktailRatingDTO
    {
        public Guid CocktailId { get; set; }
        public Guid AppUserId { get; set; }
        public int Score { get; set; }
    }
}
