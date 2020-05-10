using System;
using System.Collections.Generic;
using System.Text;
using CM.Models;

namespace CM.DTOs.Mappers.Contracts
{
    public interface IBarMapper
    {
        BarCommentDTO CreateBarCommentDTO(BarComment barComment);
        BarDTO CreateBarDTO(Bar bar);
        BarRatingDTO CreateBarRatingDTO(BarRating rating);
    }
}
