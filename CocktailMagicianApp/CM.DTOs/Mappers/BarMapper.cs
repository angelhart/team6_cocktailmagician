﻿using CM.DTOs.Mappers.Contracts;
using CM.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CM.DTOs.Mappers
{

    public class BarMapper : IBarMapper
    {
        public BarDTO CreateBarDTO(Bar bar)
        {
            return new BarDTO
            {
                Id = bar.Id,
                Name = bar.Name,
                AverageRating = bar.AverageRating,
                Address = String.Concat(bar.Address.Country.Name, ',', bar.Address.City, ',', bar.Address.Street),
                Phone = bar.Phone,
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

        private BarCocktailDTO CreateBarCocktailDTO(BarCocktail cocktail)
        {
            return new BarCocktailDTO
            {
                Id = cocktail.BarId,
                Name = cocktail.Bar?.Name
            };
        }
    }
}