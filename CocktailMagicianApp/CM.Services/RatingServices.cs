using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers.Contracts;
using CM.Models;
using CM.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services
{
    public class RatingServices : IRatingServices
    {
        private readonly CMContext _context;
        private readonly ICocktailMapper _cocktailMapper;
        private readonly IBarMapper _barMapper;

        public RatingServices(CMContext context,
                              ICocktailMapper cocktailMapper,
                              IBarMapper barMapper)
        {
            this._context = context;
            this._cocktailMapper = cocktailMapper;
            this._barMapper = barMapper;
        }
        public async Task<BarRatingDTO> RateBarAsync(int id, BarRatingDTO barRatingDTO)
        {
            throw new NotImplementedException();
        }

        private async Task<CocktailRating> GetCocktailRatingEntityAsync(Guid userId, Guid cocktailId)
        {
            var entry = await _context.CocktailRatings
                                      .FirstOrDefaultAsync(cr => cr.AppUserId == userId
                                                                 && cr.CocktailId == cocktailId);
            if (entry == null)
                throw new NullReferenceException("Cocktail rating not found.");

            return entry;
        }

        private async Task UpdateCocktailAverageRatingAsync(Guid cocktailId)
        {
            // update average in the cocktail entity
            var cocktail = await _context.Cocktails.FindAsync(cocktailId) 
                ?? throw new NullReferenceException("No cocktail for which to update the rating found");
            cocktail.AverageRating = await _context.CocktailRatings
                                                .Where(cr => cr.CocktailId == cocktailId)
                                                .AverageAsync(cr => cr.Score);
            _context.Update(cocktail);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Adds new rating from a user for a cocktail.
        /// </summary>
        /// <param name="input">Object containing IDs for user, cocktail and score.</param>
        /// <returns><see cref="CocktailRatingDTO"/></returns>
        public async Task<CocktailRatingDTO> RateCocktailAsync(CocktailRatingDTO input)
        {
            if (input == null)
                throw new ArgumentNullException("New rating object cannot be null");

            var newRating = new CocktailRating
            {
                AppUserId = input.AppUserId,
                CocktailId = input.CocktailId,
                Score = input.Score
            };

            await _context.AddAsync(newRating);
            await _context.SaveChangesAsync();

            await UpdateCocktailAverageRatingAsync(input.CocktailId);

            var outputDto = _cocktailMapper.CreateCocktailRatingDTO(newRating);

            return outputDto;
        }

        /// <summary>
        /// Gets the rating left by a user to a cocktail.
        /// </summary>
        /// <param name="userId">Id of user.</param>
        /// <param name="cocktailId">Id of cocktail.</param>
        /// <returns><see cref="CocktailRatingDTO"/></returns>
        public async Task<CocktailRatingDTO> GetCocktailRatingAsync(Guid userId, Guid cocktailId)
        {
            var entry = await GetCocktailRatingEntityAsync(userId, cocktailId);

            var outputDto = _cocktailMapper.CreateCocktailRatingDTO(entry);

            return outputDto;
        }

        /// <summary>
        /// Edits the of a rating from a user for a given cocktail.
        /// </summary>
        /// <param name="input">Object containing IDs for user, cocktail and score.</param>
        /// <returns><see cref="CocktailRatingDTO"/></returns>
        public async Task<CocktailRatingDTO> EditCocktailRatingAsync(CocktailRatingDTO input)
        {
            if (input == null)
                throw new ArgumentNullException("Input object cannot be null.");

            var entry = await GetCocktailRatingEntityAsync(input.AppUserId, input.CocktailId);

            entry.Score = input.Score;
            _context.Update(entry);
            await _context.SaveChangesAsync();

            await UpdateCocktailAverageRatingAsync(input.CocktailId);

            var outputDto = _cocktailMapper.CreateCocktailRatingDTO(entry);

            return outputDto;
        }

        /// <summary>
        /// Deletes a rating from a user for a given cocktail.
        /// </summary>
        /// <param name="input">Object containing IDs for user, cocktail.</param>
        /// <returns><see cref="CocktailRatingDTO"/></returns>
        public async Task<CocktailRatingDTO> DeleteCocktailRatingAsync(Guid userId, Guid cocktailId)
        {
            var entry = await GetCocktailRatingEntityAsync(userId, cocktailId);

            var outputDto = _cocktailMapper.CreateCocktailRatingDTO(entry);
            _context.Remove(entry);

            await UpdateCocktailAverageRatingAsync(cocktailId);

            await _context.SaveChangesAsync();

            return outputDto;
        }
    }
}
