using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers.Contracts;
using CM.Models;
using CM.Services.Contracts;
using CM.Services.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Services
{
    public class BarServices : IBarServices
    {
        private readonly CMContext _context;
        private readonly IBarMapper _barMapper;
        private readonly IAddressServices _addressServices;

        public BarServices(CMContext context, IBarMapper barMapper, IAddressServices addressServices)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _barMapper = barMapper ?? throw new ArgumentNullException(nameof(barMapper));
            _addressServices = addressServices ?? throw new ArgumentNullException(nameof(addressServices));
        }


        /// <summary>
        /// Retrieves a collection of all bars in the database.
        /// </summary>
        /// <returns>ICollection</returns>
        public async Task<PaginatedList<BarDTO>> GetAllBarsAsync(string searchString = "", int pageNumber = 1, int pageSize = 10,
                                                                string sortBy = "", string sortOrder = "", bool allowUnlisted = false)
        {
            var bars = _context.Bars
                            .Include(bar => bar.Address)
                                .ThenInclude(a => a.City)
                                    .ThenInclude(c => c.Country)
                            .Include(bar => bar.Ratings)
                            .Where(bar => !bar.IsUnlisted || allowUnlisted);

            if (!String.IsNullOrEmpty(searchString))
            {
                if (Double.TryParse(searchString, out double number))
                {
                    bars = bars.Where(bar => bar.AverageRating.Equals(number));
                }
                else
                {
                    bars = bars.Where(bar => bar.Name.Contains(searchString)
                                                    || bar.FullAddress.Contains(searchString));
                }
            }

            bars = SortBarsAsync(bars, sortBy, sortOrder);

            var filteredBarsCount = await bars.CountAsync();

            var filteredBarsList = await bars.Skip((pageNumber - 1) * pageSize)
                                             .Take(pageSize)
                                             .ToListAsync();

            var dtos = filteredBarsList.Select(bar => _barMapper.CreateBarDTO(bar)).ToList();

            var pagedDtos = await PaginatedList<BarDTO>.CreateAsync(dtos, pageNumber, pageSize);

            pagedDtos.SourceItems = filteredBarsCount;

            return pagedDtos;
        }


        /// <summary>
        /// Returns Top Rated Bars.
        /// </summary>
        /// <param name="ammount">The number of bars o retrieve.</param>
        /// <returns>Returns ICollection of BarDTO of the Top Rated bars.</returns>
        public async Task<ICollection<BarDTO>> GetTopBarsAsync(int ammount = 3)
        {
            var topBars = await _context.Bars
                                     .Where(bar => bar.IsUnlisted == false)
                                     .Include(bar => bar.Ratings)
                                     .Include(bar => bar.Address)
                                        .ThenInclude(a => a.City)
                                            .ThenInclude(c => c.Country)
                                                 .OrderByDescending(bar => bar.AverageRating)
                                     .Take(ammount)
                                     .ToListAsync();

            var topBarsDTO = topBars.Select(bar => _barMapper.CreateBarDTO(bar)).ToList();

            return topBarsDTO;
        }

        /// <summary>
        /// Retrieves a specific bar by given Id.
        /// </summary>
        /// <param name="id">Guid representing Id.</param>
        /// <returns>BarDTO</returns>
        public async Task<BarDTO> GetBarAsync(Guid id)
        {
            var bar = await _context.Bars
                        .Include(bar => bar.Address)
                            .ThenInclude(a => a.City)
                                .ThenInclude(c => c.Country)
                        .Include(bar => bar.Cocktails)
                            .ThenInclude(bc => bc.Cocktail)
                        .Include(bar => bar.Comments)
                            .ThenInclude(comment => comment.AppUser)
                        .FirstOrDefaultAsync(bar => bar.Id == id);

            if (bar == null)
            {
                throw new ArgumentException("The Bar you were looking for was not found!");
            }

            var barDTO = this._barMapper.CreateBarDTO(bar);

            return barDTO;
        }

        /// <summary>
        /// Retrieves a collection of all bars that offer specific cocktail.
        /// </summary>
        /// <returns>ICollection</returns>
        public IQueryable<Bar> GetBarsByCocktail(Guid targetCocktailId, bool allowUnlisted = false)
        {
            if (targetCocktailId.Equals(Guid.Empty))
            {
                throw new ArgumentException("No cocktail id specified!");
            }
            var bars = _context.Bars
                            .Include(bar => bar.Cocktails)
                                .ThenInclude(c => c.Cocktail)
                            .Where(bar => bar.Cocktails.Any(x => x.CocktailId == targetCocktailId)
                                          && (!bar.IsUnlisted || allowUnlisted));
            return bars;
        }

        /// <summary>
        /// Retrieves a collection of all bars in the database.
        /// </summary>
        /// <returns>ICollection</returns>
        public async Task<PaginatedList<BarDTO>> GetBarsWithCocktailsAsync(string searchString = "", int pageNumber = 1, int pageSize = 10,
                                                                string sortBy = "", string sortOrder = "", bool allowUnlisted = false)
        {
            var bars = _context.Bars
                            .Include(bar => bar.Cocktails)
                                .ThenInclude(c => c.Cocktail)
                            .Where(bar => !bar.IsUnlisted || allowUnlisted);

            if (!String.IsNullOrEmpty(searchString))
            {
                bars = bars.Where(bar => bar.Name.Contains(searchString)
                                         || bar.Cocktails.Any(c => c.Cocktail.Name.Contains(searchString)));
            }

            bars = SortBarsAsync(bars, sortBy, sortOrder);

            var filteredBarsCount = await bars.CountAsync();

            var filteredBarsList = await bars.Skip((pageNumber - 1) * pageSize)
                                             .Take(pageSize)
                                             .ToListAsync();

            var dtos = filteredBarsList.Select(bar => _barMapper.CreateBarMenuDTO(bar)).ToList();

            var pagedDtos = await PaginatedList<BarDTO>.CreateAsync(dtos, pageNumber, pageSize);

            pagedDtos.SourceItems = filteredBarsCount;

            return pagedDtos;
        }

        /// <summary>
        /// Updates the current state of a bar.
        /// </summary>
        /// <param name="id">Guid representing Id.</param>
        /// <param name="barDTO">The new params to be updated in the specific Bar.</param>
        /// <returns></returns>
        public async Task<BarDTO> UpdateBarAsync(Guid id, BarDTO barDTO)
        {
            var bar = await _context.Bars
                        .FirstOrDefaultAsync(bar => bar.Id == id);

            if (bar == null || barDTO == null)
            {
                throw new ArgumentNullException();
            }

            bar.Name = barDTO.Name;
            bar.Phone = barDTO.Phone;
            bar.Details = barDTO.Details;
            if (barDTO.ImagePath != null)
            {
                bar.ImagePath = barDTO.ImagePath;
            }

            if (barDTO.Cocktails == null || barDTO.Cocktails.Count == 0)
                throw new ArgumentException("Choose cocktails!");

            await UpdateCocktailsInBarAsync(bar.Id, barDTO.Cocktails);

            _context.Update(bar);
            await _context.SaveChangesAsync();

            return barDTO;
        }

        /// <summary>
        /// Creates a new Bar.
        /// </summary>
        /// <param name="barDTO">The params needed to create a bar. </param>
        /// <returns>BarDTO</returns>
        public async Task<BarDTO> CreateBarAsync(BarDTO barDTO)
        {
            if (barDTO.Name == null)
                throw new ArgumentNullException("Name cannot be null!");

            var barExisting = await _context.Bars
                            .Include(bar => bar.Address)
                                .ThenInclude(a => a.City)
                            .Where(bar => bar.Name == barDTO.Name && bar.Address.CityId == barDTO.Address.CityId)
                            .ToArrayAsync();

            if (barExisting.Length > 0)
                throw new DbUpdateException("Bar with the same name already exists in this City!");

            try
            {

                var bar = new Bar
                {
                    Id = Guid.NewGuid(),
                    Name = barDTO.Name,
                    Phone = barDTO.Phone,
                    ImagePath = barDTO.ImagePath,
                    Details = barDTO.Details,
                };

                barDTO.Address.BarId = bar.Id;
                await _context.Bars.AddAsync(bar);
                await _context.SaveChangesAsync();

                var addressDTO = await _addressServices.CreateAddressAsync(barDTO.Address);
                bar.AddressID = addressDTO.Id;
                bar.FullAddress = $"{addressDTO.CityName}, {addressDTO.Street}";

                var cocktails = barDTO.Cocktails.Select(sc =>
                new BarCocktail
                {
                    BarId = bar.Id,
                    CocktailId = sc.Id
                });

                foreach (var item in cocktails)
                {
                    await _context.BarCocktails.AddAsync(item);
                }

                await _context.SaveChangesAsync();

                return barDTO;
            }
            catch (Exception)
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Marks specified bar in the database as deleted.
        /// </summary>
        /// <param name="id">The Id of the bar that should be marked as deleted.</param>
        /// <returns>BarDTO</returns>
        public async Task DeleteBar(Guid id)
        {
            var bar = await _context.Bars
                .FirstOrDefaultAsync(bar => bar.Id == id && bar.IsUnlisted == false) ?? throw new ArgumentNullException();

            bar.IsUnlisted = true;

            _context.Bars.Update(bar);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if a bar exists in the database by given Id.
        /// </summary>
        /// <param name="id">The Id of the bar to be checked.</param>
        /// <returns></returns>
        public async Task<bool> BarExists(Guid id)
        {
            return await _context.Bars.AnyAsync(e => e.Id == id);
        }

        /// <summary>
        /// Counts the bar records in the database.
        /// </summary>
        /// <param name="countUnlisted">Set to true to include unlisted bars</param>
        /// <returns>Number of bars in database.</returns>
        public async Task<int> CountAllBarsAsync(bool countUnlisted = false)
        {
            return await _context.Bars
                                 .Where(b => !b.IsUnlisted || countUnlisted)
                                 .CountAsync();
        }


        /// <summary>
        /// Changes the unlisted state of a bar.
        /// </summary>
        /// <param name="barId">ID of the bar.</param>
        /// <param name="newState">The new state for property.</param>
        /// <param name="allowUnlisted">Checks if caller method is permited to retrieve unlisted cocktail.</param>
        /// <returns></returns>
        public async Task<BarDTO> ChangeListingAsync(Guid barId)
        {
            var bar = await _context.Bars
            .FirstOrDefaultAsync(bar => bar.Id == barId);

            if (bar == null)
            {
                throw new ArgumentException("The Bar you were looking for was not found!");
            }

            bar.IsUnlisted = !bar.IsUnlisted;


            _context.Update(bar);
            await _context.SaveChangesAsync();

            var barDTO = this._barMapper.CreateBarDTO(bar);

            return barDTO;
        }

        private IQueryable<Bar> SortBarsAsync(IQueryable<Bar> bars, string sortBy, string sortOrder)
        {
            return sortBy switch
            {
                "averageRating" => string.IsNullOrEmpty(sortOrder) ? bars.OrderBy(bar => bar.AverageRating)
                                                                       .ThenBy(bar => bar.Name) :
                                                              bars.OrderByDescending(bar => bar.AverageRating)
                                                                       .ThenBy(bar => bar.Name),
                "country" => string.IsNullOrEmpty(sortOrder) ? bars.OrderBy(bar => bar.Address.City.Country)
                                                                        .ThenBy(bar => bar.Name) :
                                                              bars.OrderByDescending(bar => bar.Address.City.Country)
                                                                        .ThenBy(bar => bar.Name),
                "city" => string.IsNullOrEmpty(sortOrder) ? bars.OrderBy(bar => bar.Address.City)
                                                                        .ThenBy(bar => bar.Name) :
                                                              bars.OrderByDescending(bar => bar.Address.City)
                                                                        .ThenBy(bar => bar.Name),

                _ => string.IsNullOrEmpty(sortOrder) ? bars.OrderBy(bar => bar.Name) :
                                                       bars.OrderByDescending(bar => bar.Name),
            };
        }
        private async Task UpdateCocktailsInBarAsync(Guid barId, ICollection<CocktailDTO> newCocktails)
        {
            var currentCocktails = _context.BarCocktails
                                             .Where(bc => bc.BarId == barId);

            _context.RemoveRange(currentCocktails);

            var cocktailsToAdd = newCocktails.Select(cocktailDTO => new BarCocktail
            {
                BarId = barId,
                CocktailId = cocktailDTO.Id,
            });

            await _context.AddRangeAsync(cocktailsToAdd);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Retrieves the Count of all Bar records in the database.
        /// </summary>
        /// <returns>Integer of all bars in the database.</returns>
    }
}
