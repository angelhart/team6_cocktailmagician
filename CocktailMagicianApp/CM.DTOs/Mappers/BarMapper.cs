using CM.DTOs.Mappers.Contracts;
using CM.Models;
using System;
using System.Linq;

namespace CM.DTOs.Mappers
{

    public class BarMapper : IBarMapper
    {
        private readonly IAddressMapper _addressMapper;

        public BarMapper(IAddressMapper addressMapper)
        {
            _addressMapper = addressMapper ?? throw new ArgumentNullException(nameof(addressMapper));
        }


        public BarDTO CreateBarDTO(Bar bar)
        {
            var barDTO = new BarDTO
            {
                Id = bar.Id,
                Name = bar.Name,
                AverageRating = bar.AverageRating,
                ImagePath = bar.ImagePath,

                Address = this._addressMapper.CreateAddressDTO(bar.Address),
                FullAddress = bar.FullAddress,
                Phone = bar.Phone,
                Details = bar.Details,
                IsUnlisted = bar.IsUnlisted,

                Cocktails = bar.Cocktails
                        .Select(bc => CreateCocktailDTO(bc.Cocktail))
                        .ToList(),
                Comments = bar.Comments
                        .Select(barComment => CreateBarCommentDTO(barComment))
                        .OrderByDescending(comment => comment.CommentedOn)
                        .ToList(),
            };

            if (string.IsNullOrEmpty(barDTO.ImagePath))
                barDTO.ImagePath = "/images/DefaultBar.png";

            return barDTO;
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
                BarId = rating.BarId,
                BarName = rating.Bar.Name,
                Score = rating.Score
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

        private CocktailDTO CreateCocktailDTO(Cocktail cocktail)
        {
            var cocktailDTO = new CocktailDTO
            {
                Id = cocktail.Id,
                Name = cocktail.Name,
                Recipe = cocktail.Recipe,
                IsUnlisted = cocktail.IsUnlisted,
                AverageRating = cocktail.AverageRating,
                ImagePath = cocktail.ImagePath,
            };

            if (string.IsNullOrEmpty(cocktailDTO.ImagePath))
                cocktailDTO.ImagePath = "/images/DefaultCocktail.png";

            return cocktailDTO;
        }
    }
}
