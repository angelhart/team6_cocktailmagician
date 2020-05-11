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

        private async Task<Cocktail> GetCocktailAsync(Guid id, bool isAdmin = false)
        {
            if (id == null)
                throw new ArgumentNullException("Cocktail ID can't be null.");

            var cocktail = await _context.Cocktails
                                         .Include(c => c.Bars)
                                         .Include(c => c.Comments)
                                             .ThenInclude(com => com.AppUser)
                                         .Include(c => c.Ingredients)
                                             .ThenInclude(i => i.Ingredient)
                                         .FirstOrDefaultAsync(c => c.Id == id);

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
        public async Task<CocktailDTO> GetCocktailDetailsAsync(Guid cocktailId, bool isAdmin = false)
        {
            var cocktail = await GetCocktailAsync(cocktailId, isAdmin);

            var outputDto = _cocktailMapper.CreateCocktailDTO(cocktail);

            return outputDto;
        }

        /// <summary>
        /// Adds new entry in Cocktails table and its ingredients in CocktailIngredients table.
        /// </summary>
        /// <param name="dto">Carries the data from which to create the new cocktail.</param>
        /// <returns><see cref="CocktailDTO"/>.</returns>
        public async Task<CocktailDTO> CreateCocktailAsync(CocktailDTO dto)
        {
            var nameExists = await _context.Cocktails.AnyAsync(c => c.Name == dto.Name);
            if (nameExists)
                throw new DbUpdateException("Cocktail with this name already exists in the records.");

            var cocktail = _cocktailMapper.CreateCocktail(dto);
            await _context.Cocktails.AddAsync(cocktail);
            await _context.SaveChangesAsync();

            await _ingredientServices.AddIngredientsToCocktailAsync(cocktail.Id, dto.Ingredients);

            var outputDto = await GetCocktailDetailsAsync(cocktail.Id);

            return outputDto;
        }

        /// <summary>
        /// Updates existing cocktail information.
        /// </summary>
        /// <param name="cocktailId">ID of the cocktail.</param>
        /// <param name="dto">Carries the updated data.</param>
        /// <param name="isAdmin">Checks if caller method is permited to retrieve unlisted cocktail.</param>
        /// <returns><see cref="CocktailDTO"/>.</returns>
        public async Task<CocktailDTO> UpdateCocktailAsync(CocktailDTO dto, bool isAdmin = false)
        {
            var cocktail = await GetCocktailAsync(dto.Id, isAdmin);

            cocktail.Name = dto.Name;
            cocktail.Recipe = dto.Recipe;
            // TODO: cocktail.Picture
            await _ingredientServices.UpdateCocktailIngredientsAsync(dto.Id, dto.Ingredients);

            _context.Cocktails.Update(cocktail);
            await _context.SaveChangesAsync();

            var outputDto = _cocktailMapper.CreateCocktailDTO(cocktail);
            return outputDto;
        }

        /// <summary>
        /// Changes the unlisted state of a cocktail.
        /// </summary>
        /// <param name="cocktailId">ID of the cocktail.</param>
        /// <param name="isUnlisted">The new state variable</param>
        /// <param name="isAdmin">Checks if caller method is permited to retrieve unlisted cocktail.</param>
        /// <returns></returns>
        public async Task<CocktailDTO> CocktailListingAsync(Guid cocktailId, bool isUnlisted, bool isAdmin = false)
        {
            var cocktail = await GetCocktailAsync(cocktailId, isAdmin);

            cocktail.IsUnlisted = isUnlisted;
            await _context.SaveChangesAsync();

            var outputDto = _cocktailMapper.CreateCocktailDTO(cocktail);
            return outputDto;
        }

        public async Task<ICollection<CocktailHomePageDTO>> GetTopCocktailsAsync(int ammount = 3)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<CocktailSearchDTO>> SearchCocktailsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
