using CM.DTOs.Mappers.Contracts;
using CM.Models;
using System;
using System.Linq;

namespace CM.DTOs.Mappers
{

    public class BarMapper : IBarMapper
    {
        private readonly IAddressMapper _addressMapper;
        private readonly ICocktailMapper _cocktailMapper;

        public BarMapper(IAddressMapper addressMapper, ICocktailMapper cocktailMapper)
        {
            _addressMapper = addressMapper ?? throw new ArgumentNullException(nameof(addressMapper));
            _cocktailMapper = cocktailMapper ?? throw new ArgumentNullException(nameof(cocktailMapper));
        }


        public BarDTO CreateBarDTO(Bar bar)
        {
            return new BarDTO
            {
                Id = bar.Id,
                Name = bar.Name,
                AverageRating = bar.AverageRating,

                Address = this._addressMapper.CreateAddressDTO(bar.Address),
                Phone = bar.Phone,
                Details = bar.Details,
                IsUnlisted = bar.IsUnlisted,

                Cocktails = bar.Cocktails
                        .Select(bc => _cocktailMapper.CreateCocktailDTO(bc.Cocktail))
                        .ToList(),
                Comments = bar.Comments
                        .Select(barComment => CreateBarCommentDTO(barComment))
                        .ToList(),
            };
        }

        public BarCommentDTO CreateBarCommentDTO(BarComment barComment)
        {
            return new BarCommentDTO
            {
                BarId = barComment.BarId,
                BarName = barComment.Bar?.Name,
                UserId = barComment.AppUserId,
                UserName = barComment.AppUser?.UserName,
                CommentedOn = barComment.CommentedOn,
                Text = barComment.Text
            };
        }

        public BarRatingDTO CreateBarRatingDTO(BarRating rating)
        {
            return new BarRatingDTO
            {
                AppUserId = rating.AppUserId,
                AppUserName = rating.AppUser.UserName,
                BarId = rating.BarId,
                BarName = rating.Bar.Name,
                Score = rating.Score
            };
        }

        private BarCocktailDTO CreateBarCocktailDTO(BarCocktail bc)
        {
            return new BarCocktailDTO
            {
                BarId = bc.BarId,
                Bar = bc.Bar?.Name,
                CocktailId = bc.CocktailId,
                Cocktail = bc.Cocktail?.Name
            };
        }

        public BarComment CreateBarComment(BarCommentDTO newCommentDto)
        {
            return new BarComment
            {
                BarId = newCommentDto.BarId,
                AppUserId = newCommentDto.UserId,
                CommentedOn = newCommentDto.CommentedOn,
                Text = newCommentDto.Text
            };
        }
    }
}
