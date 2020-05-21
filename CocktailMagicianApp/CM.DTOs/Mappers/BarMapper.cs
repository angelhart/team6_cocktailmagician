﻿using CM.DTOs.Mappers.Contracts;
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
            _addressMapper = addressMapper ?? throw new ArgumentNullException(nameof(_addressMapper));
        }


        public BarDTO CreateBarDTO(Bar bar)
        {
            return new BarDTO
            {
                Id = bar.Id,
                Name = bar.Name,
                AverageRating = bar.AverageRating,
                //Address = String.Concat(bar.Address.Country.Name, ',', bar.Address.City, ',', bar.Address.Street),

                Address = this._addressMapper.CreateAddressDTO(bar.Address),
                Phone = bar.Phone,
                Details = bar.Details,

                Cocktails = bar.Cocktails
                        .Select(cocktail => CreateBarCocktailDTO(cocktail))
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
                Bar = bc.Bar,
                CocktailId = bc.CocktailId,
                Cocktail = bc.Cocktail
            };
        }

        public Bar CreateBar(BarDTO barDTO, Address address)
        {
            return new Bar
            {
                Name = barDTO.Name,
                Phone = barDTO.Phone,
                ImagePath = barDTO.ImagePath,
                Details = barDTO.Details,
                Address = address,
            };
        }

        public BarCocktail CreateBarCocktail(Bar bar, Cocktail cocktail)
        {
            return new BarCocktail
            {
                BarId = bar.Id,
                Bar = bar,
                CocktailId = cocktail.Id,
                Cocktail = cocktail
            };
        }
    }
}
