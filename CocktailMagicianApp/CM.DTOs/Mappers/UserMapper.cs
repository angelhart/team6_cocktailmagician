using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CM.DTOs.Mappers.Contracts;
using CM.Models;

namespace CM.DTOs.Mappers
{

    public class UserMapper : IUserMapper
    {
        private readonly IBarMapper _barMapper;
        private readonly ICocktailMapper _cocktailMapper;
        public UserMapper(IBarMapper barMapper, ICocktailMapper cocktailMapper)
        {
            this._barMapper = barMapper ?? throw new ArgumentNullException(nameof(barMapper));
            this._cocktailMapper = cocktailMapper ?? throw new ArgumentNullException(nameof(cocktailMapper));
        }
        public AppUserDTO CreateAppUserDTO(AppUser user)
        {
            return new AppUserDTO
            {
                Id = user.Id,
                ImagePath = user.ImagePath,
                IsDeleted = user.IsDeleted,

                BarRatings = user.BarRatings
                            .Select(rating => _barMapper.CreateBarRatingDTO(rating)).ToList(),
                BarComments = user.BarComments
                            .Select(comment => _barMapper.CreateBarCommentDTO(comment)).ToList(),

                CoctailRatings = user.CocktailRatings
                            .Select(rating => _cocktailMapper.CreateCocktailRatingDTO(rating)).ToList(),
                CocktailComments = user.CocktailComments
                            .Select(comment => _cocktailMapper.CreateCocktailCommentDTO(comment)).ToList()
            };
        }
        public AppUser CreateAppUser()
        {
            return new AppUser();
        }
    }
}
