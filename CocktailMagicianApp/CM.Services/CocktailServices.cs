using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers;
using CM.DTOs.Mappers.Contracts;
using CM.Models;
using CM.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services
{
    public class CocktailServices : ICocktailServices
    {
        private readonly CMContext _context;
        private readonly ICocktailMapper _cocktailMapper;
        private readonly IIngredientServices _ingredientServices;

        public CocktailServices(CMContext cmContext,
                                ICocktailMapper cocktailMapper,
                                IIngredientServices ingredientServices)
        {
            this._context = cmContext;
            this._cocktailMapper = cocktailMapper;
            this._ingredientServices = ingredientServices;
        }

        private async Task<Cocktail> GetCocktail(Guid cocktailId, bool isAdmin = false)
        {
            if (cocktailId == null)
                throw new ArgumentNullException("Cocktail ID can't be null.");

            var cocktail = await _context.Cocktails
                                         .Include(c => c.Bars)
                                         .Include(c => c.Comments)
                                             .ThenInclude(com => com.AppUser)
                                         .Include(c => c.Ingredients)
                                             .ThenInclude(i => i.Ingredient)
                                         .FirstOrDefaultAsync(c => c.Id == cocktailId);

            if (cocktail == null)
                throw new KeyNotFoundException("No cocktail found with the specified ID.");

            if (cocktail.IsUnlisted && !isAdmin)
                throw new UnauthorizedAccessException("Only administrators can access details of unlisted cocktails.");

            return cocktail;
        }

        /// <summary>
        /// Gets full details for a single cocktail if it's listed, or unlisted and queried by admin.
        /// </summary>
        /// <param name="isAdmin">Checks if caller method is permited to retrieve unlisted cocktail.</param>
        /// <param name="cocktailId">ID of the cocktail.</param>
        /// <returns><see cref="CocktailDTO"/></returns>
        public async Task<CocktailDTO> GetCocktailDetails(Guid cocktailId, bool isAdmin = false)
        {
            var cocktail = await GetCocktail(cocktailId, isAdmin);

            var output = GetCocktailDetails(cocktail);

            return output;
        }

        private CocktailDTO GetCocktailDetails(Cocktail cocktail)
        {
            var output = _cocktailMapper.CreateCocktailDTO(cocktail);

            return output;
        }

        /// <summary>
        /// Adds new entry in Cocktails table and its ingredients in CocktailIngredients table.
        /// </summary>
        /// <param name="dto">Carries the data from which to create the new cocktail.</param>
        /// <returns>Full details using <see cref="GetCocktailDetails"/>.</returns>
        public async Task<CocktailDTO> CreateCocktail(CocktailDTO dto)
        {
            var nameExists = await _context.Cocktails.AnyAsync(c => c.Name == dto.Name);
            if (nameExists)
                throw new DbUpdateException("Cocktail with this name already exists in the records.");

            var cocktail = _cocktailMapper.CreateCocktail(dto);
            await _context.Cocktails.AddAsync(cocktail);
            await _context.SaveChangesAsync();

            await _ingredientServices.AddIngredientsToCocktail(cocktail.Id, dto.Ingredients);

            var output = await GetCocktailDetails(cocktail.Id);

            return output;
        }

        /// <summary>
        /// Updates existing cocktail information.
        /// </summary>
        /// <param name="cocktailId">ID of the cocktail.</param>
        /// <param name="dto">Carries the updated data.</param>
        /// <param name="isAdmin">Checks if caller method is permited to retrieve unlisted cocktail.</param>
        /// <returns>Full details using <see cref="GetCocktailDetails"/>.</returns>
        public async Task<CocktailDTO> UpdateCocktail(Guid cocktailId, CocktailDTO dto, bool isAdmin = false)
        {
            var cocktail = await GetCocktail(cocktailId, isAdmin);

            cocktail.Name = dto.Name;
            cocktail.Recipe = dto.Recipe;
            // TODO: cocktail.Picture
            await _ingredientServices.UpdateIngredients(cocktailId, dto.Ingredients);

            var output = await GetCocktailDetails(cocktail);
            return output;
        }

        /// <summary>
        /// Changes the unlisted state of a cocktail.
        /// </summary>
        /// <param name="cocktailId">ID of the cocktail.</param>
        /// <param name="isUnlisted">The new state variable</param>
        /// <param name="isAdmin">Checks if caller method is permited to retrieve unlisted cocktail.</param>
        /// <returns></returns>
        public async Task<CocktailDTO> CocktailListing(Guid cocktailId, bool isUnlisted, bool isAdmin = false)
        {
            var cocktail = await GetCocktail(cocktailId, isAdmin);

            cocktail.IsUnlisted = isUnlisted;
            await _context.SaveChangesAsync();

            var output = GetCocktailDetails(cocktail);
            return output;
        }

        public async Task<ICollection<CocktailHomePageDTO>> GetTopCocktails(int ammount = 3)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<CocktailSearchDTO>> SearchCocktails()
        {
            throw new NotImplementedException();
        }
    }
}
