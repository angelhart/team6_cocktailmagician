using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers;
using CM.DTOs.Mappers.Contracts;
using CM.Models;
using CM.Services.Contracts;
using CM.Services.Providers;
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

        public CocktailServices(CMContext cmContext,
                                ICocktailMapper cocktailMapper)
        {
            this._context = cmContext;
            this._cocktailMapper = cocktailMapper;
        }

        private async Task<Cocktail> GetCocktailAsync(Guid id, bool allowUnlisted = false)
        {
            //if (id == null)
            //    throw new ArgumentNullException("Cocktail ID can't be null.");

            var cocktail = await _context.Cocktails
                                         .Include(c => c.Bars)
                                         .Include(c => c.Ratings)
                                         .Include(c => c.Comments)
                                             .ThenInclude(com => com.AppUser)
                                         .Include(c => c.Ingredients)
                                             .ThenInclude(i => i.Ingredient)
                                         .FirstOrDefaultAsync(c => c.Id == id);

            if (cocktail == null)
                throw new KeyNotFoundException("No cocktail found with the specified ID.");

            if (cocktail.IsUnlisted && !allowUnlisted)
                throw new UnauthorizedAccessException("Only administrators can access details of unlisted cocktails.");

            return cocktail;
        }
        
        private async Task UpdateIngredientsAsync(Guid id, ICollection<CocktailIngredientDTO> newIngredients)
        {
            var currentIngredients = _context.CocktailIngredients
                                             .Where(ci => ci.CocktailId == id);

            _context.RemoveRange(currentIngredients);

            var ingredientsToAdd = newIngredients.Select(ci => _cocktailMapper.CreateCocktailIngredient(id, ci));

            await _context.AddRangeAsync(ingredientsToAdd);
            await _context.SaveChangesAsync();
        }
        
        private IQueryable<Cocktail> SortCocktails(IQueryable<Cocktail> cocktails, string sortBy, string sortOrder)
        {
            return sortBy switch
            {
                "rating" => string.IsNullOrEmpty(sortOrder) ? cocktails.OrderBy(c => c.AverageRating)
                                                                       .ThenBy(c => c.Name) :
                                                              cocktails.OrderByDescending(c => c.AverageRating)
                                                                       .ThenBy(c => c.Name),

                _ => string.IsNullOrEmpty(sortOrder) ? cocktails.OrderBy(c => c.Name) :
                                                       cocktails.OrderByDescending(c => c.Name),
            };
        }

        /// <summary>
        /// Gets full details for a single cocktail if it's listed, or unlisted and queried by admin.
        /// </summary>
        /// <param name="allowUnlisted">Checks if caller method is permited to retrieve unlisted cocktail.</param>
        /// <param name="cocktailId">ID of the cocktail.</param>
        /// <returns><see cref="CocktailDTO"/></returns>
        public async Task<CocktailDTO> GetCocktailDetailsAsync(Guid cocktailId, bool allowUnlisted = false)
        {
            var cocktail = await GetCocktailAsync(cocktailId, allowUnlisted);

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

            await UpdateIngredientsAsync(cocktail.Id, dto.Ingredients);

            var outputDto = await GetCocktailDetailsAsync(cocktail.Id);

            return outputDto;
        }

        /// <summary>
        /// Updates existing cocktail information.
        /// </summary>
        /// <param name="cocktailId">ID of the cocktail.</param>
        /// <param name="dto">Carries the updated data.</param>
        /// <param name="allowUnlisted">Checks if caller method is permited to retrieve unlisted cocktail.</param>
        /// <returns><see cref="CocktailDTO"/>.</returns>
        public async Task<CocktailDTO> UpdateCocktailAsync(CocktailDTO dto)
        {
            var cocktail = await GetCocktailAsync(dto.Id, allowUnlisted: true);

            cocktail.Name = dto.Name;
            cocktail.Recipe = dto.Recipe;
            // TODO: cocktail.Picture
            await UpdateIngredientsAsync(cocktail.Id, dto.Ingredients);

            _context.Cocktails.Update(cocktail);
            await _context.SaveChangesAsync();

            var outputDto = _cocktailMapper.CreateCocktailDTO(cocktail);
            return outputDto;
        }

        /// <summary>
        /// Changes the unlisted state of a cocktail.
        /// </summary>
        /// <param name="cocktailId">ID of the cocktail.</param>
        /// <param name="newState">The new state for property.</param>
        /// <param name="allowUnlisted">Checks if caller method is permited to retrieve unlisted cocktail.</param>
        /// <returns></returns>
        public async Task<CocktailDTO> ChangeListingAsync(Guid cocktailId, bool newState)
        {
            var cocktail = await GetCocktailAsync(cocktailId, allowUnlisted: true);

            cocktail.IsUnlisted = newState;
            await _context.SaveChangesAsync();

            var outputDto = _cocktailMapper.CreateCocktailDTO(cocktail);
            return outputDto;
        }

        /// <summary>
        /// Retrieves the highest rated, listed cocktails.
        /// </summary>
        /// <param name="ammount">The number of cocktails to retrieve.</param>
        /// <returns><see cref="ICollection<<see cref="CocktailHomePageDTO"/>>"/></returns>
        public async Task<ICollection<CocktailDTO>> GetTopCocktailsAsync(int ammount = 3)
        {
            var topCocktails = await _context.Cocktails
                                     .Where(c => !c.IsUnlisted)
                                     .OrderByDescending(c => c.AverageRating)
                                     .Take(ammount)
                                     .Select(c => _cocktailMapper.CreateCocktailDTO(c))
                                     .ToListAsync();

            return topCocktails;
        }

        /// <summary>
        /// Returns paged results of searched cocktails.
        /// </summary>
        /// <param name="searchString">Filter condition.</param>
        /// <param name="sortBy">Property to which to sort by.</param>
        /// <param name="sortOrder">Ascending by default</param>
        /// <param name="pageNumber">The required page of the paginated list.</param>
        /// <param name="pageSize">Number of ingredients per page.</param>
        /// <param name="allowUnlisted">Set to true to include cocktails that are unlisted.</param>
        /// <returns><see cref="PaginatedList<<see cref="CocktailDTO"/>>"/></returns>
        public async Task<PaginatedList<CocktailDTO>> PageCocktailsAsync(string searchString = "",
                                                                         string sortBy = "",
                                                                         string sortOrder = "",
                                                                         int pageNumber = 1,
                                                                         int pageSize = 10,
                                                                         bool allowUnlisted = false)
        {
            var cocktails = _context.Cocktails
                                    .Include(c => c.Ingredients)
                                        .ThenInclude(i => i.Ingredient)
                                    .Include(c => c.Ratings)
                                    .Where(c => (!c.IsUnlisted || allowUnlisted)
                                                && (c.Name.Contains(searchString)
                                                || c.Ingredients.Any(ci => ci.Ingredient.Name.Contains(searchString))));

            cocktails = SortCocktails(cocktails, sortBy, sortOrder);

            var dtos = cocktails.Select(c => _cocktailMapper.CreateCocktailDTO(c));

            var pagedDtos = await PaginatedList<CocktailDTO>.CreateAsync(dtos, pageNumber, pageSize);
            
            return pagedDtos;
        }

    }
}
