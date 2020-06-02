using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers.Contracts;
using CM.Models;
using CM.Services.Contracts;
using CM.Services.Providers;
using CM.Services.Providers.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services
{
    public class IngredientServices : IIngredientServices
    {
        private readonly CMContext _context;
        private readonly IIngredientMapper _ingredientMapper;
        //private readonly IPaginatedList _paginatedList;

        public IngredientServices(CMContext context,
                                  IIngredientMapper ingredientMapper)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this._ingredientMapper = ingredientMapper ?? throw new ArgumentNullException(nameof(ingredientMapper));
            //this._paginatedList = paginatedList ?? throw new ArgumentNullException(nameof(ingredientMapper)); 
        }

        private async Task<Ingredient> GetIngredientAsync(Guid id)
        {
            //if (id == null)
            //    throw new ArgumentNullException("Ingredient ID can't be null.");

            var ingredient = await _context.Ingredients
                                     .Include(i => i.Cocktails)
                                        .ThenInclude(ci => ci.Cocktail)
                                     .FirstOrDefaultAsync(i => i.Id == id);

            if (ingredient == null)
                throw new KeyNotFoundException("No ingredient found with the specified ID.");

            return ingredient;
        }

        /// <summary>
        /// Get all data on an ingredient, including in what cocktails it is used.
        /// </summary>
        /// <param name="id">ID of queried ingredient.</param>
        /// <returns><see cref="IngredientDTO"/></returns>
        public async Task<IngredientDTO> GetIngredientDetailsAsync(Guid id)
        {
            var ingredient = await GetIngredientAsync(id);

            var outputDto = _ingredientMapper.CreateIngredientDTO(ingredient);

            return outputDto;
        }

        /// <summary>
        /// Adds new ingredient to the database.
        /// </summary>
        /// <param name="dto">Object to take name and picture from.</param>
        /// <returns><see cref="IngredientDTO"/></returns>
        public async Task<IngredientDTO> CreateIngredientAsync(IngredientDTO dto)
        {
            var nameExists = await _context.Ingredients.AnyAsync(i => i.Name == dto.Name);
            if (nameExists)
                throw new DbUpdateException("Ingredient with this name already exists in the records.");

            Ingredient newIngredient = new Ingredient
            {
                Name = dto.Name,
                ImagePath = dto.ImagePath
            };

            await _context.Ingredients.AddAsync(newIngredient);
            await _context.SaveChangesAsync();

            var outputDto = _ingredientMapper.CreateIngredientDTO(newIngredient);
            return outputDto;
        }

        /// <summary>
        /// Updates name and picture for an ingredient.
        /// </summary>
        /// <param name="dto">Object ot take new data from.</param>
        /// <returns><see cref="IngredientDTO"/></returns>
        public async Task<IngredientDTO> UpdateIngredientAsync(IngredientDTO dto)
        {
            var ingredient = await GetIngredientAsync(dto.Id);

            ingredient.Name = dto.Name;

            if (dto.ImagePath != null)
            {
                ingredient.ImagePath = dto.ImagePath;
            }

            _context.Ingredients.Update(ingredient);
            await _context.SaveChangesAsync();

            var outputDto = _ingredientMapper.CreateIngredientDTO(ingredient);
            return outputDto;
        }

        /// <summary>
        /// Deletes ingredient from the database if it's not present in any cocktail recipes.
        /// </summary>
        /// <param name="id">ID of ingredient to be removed</param>
        /// <returns><see cref="IngredientDTO"/></returns>
        public async Task<IngredientDTO> DeleteIngredientAsync(Guid id)
        {
            var ingredient = await GetIngredientAsync(id);

            if (ingredient.Cocktails.Count != 0)
                throw new InvalidOperationException("Ingredient can't be removed, because it's part of a cocktail recipe.");

            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();

            var outputDto = _ingredientMapper.CreateIngredientDTO(ingredient);
            return outputDto;
        }

        /// <summary>
        /// Get the ingredients to show on the selected page.
        /// </summary>
        /// <param name="searchString">Filter by name condition.</param>
        /// <param name="pageNumber">The required page of the paginated list.</param>
        /// <param name="pageSize">Number of ingredients per page.</param>
        /// <returns><see cref="PaginatedList<<see cref="IngredientDTO"/>>"/></returns>
        public async Task<PaginatedList<IngredientDTO>> PageIngredientsAsync(string searchString = "", int pageNumber = 1, int pageSize = 10)
        {
            if (searchString == null)
                throw new ArgumentNullException("Search string cannot be null.");

            if (pageNumber < 1 || pageSize < 1)
                throw new ArgumentOutOfRangeException("Page number and page size must be positive integers.");

            var ingredients = _context.Ingredients
                                      .Where(i => i.Name.Contains(searchString))
                                      .Include(i => i.Cocktails)
                                      .OrderBy(i => i.Name)
                                      .Select(i => _ingredientMapper.CreateIngredientDTO(i));

            var outputDtos = await PaginatedList<IngredientDTO>.CreateAsync(ingredients, pageNumber, pageSize);

            return outputDtos;
        }

        /// <summary>
        /// Count all records in database.
        /// </summary>
        /// <returns>Integer of all entities in data base.</returns>
        public async Task<int> CountAllIngredientsAsync()
        {
            return await _context.Ingredients.CountAsync();
        }
    }
}
