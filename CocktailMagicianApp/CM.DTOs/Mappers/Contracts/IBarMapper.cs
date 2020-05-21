using System;
using System.Collections.Generic;
using System.Text;
using CM.Models;

namespace CM.DTOs.Mappers.Contracts
{
    public interface IBarMapper
    {
		Bar CreateBar(BarDTO barDTO, Address address);
		BarCocktail CreateBarCocktail(Bar bar, Cocktail cocktail);
		BarCommentDTO CreateBarCommentDTO(BarComment barComment);
        BarDTO CreateBarDTO(Bar bar);
        BarRatingDTO CreateBarRatingDTO(BarRating rating);
    }
}
