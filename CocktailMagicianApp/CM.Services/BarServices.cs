﻿using CM.Data;
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
		//public async Task<PaginatedList<BarDTO>> GetAllBarsAsync(string searchString = "", string sortBy = "", string sortOrder = "", int pageNumber = 1, int pageSize = 2, bool allowUnlisted = false)

		//var bars = _context.Bars
		//					.Include(bar => bar.Address)
		//						.ThenInclude(a => a.City)
		//							.ThenInclude(c => c.Country)
		//					.Include(bar => bar.Ratings)
		//					.Where(bar => (!bar.IsUnlisted || allowUnlisted)
		//													&& (bar.Name.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
		//														|| bar.Address.City.Name.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
		//														|| bar.Address.Street.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)));
		//---------------------------------------------------------------------------------------------------------------------------------------------------------------------
		//
		//var bars = _context.Bars
		//					.Include(bar => bar.Address)
		//						.ThenInclude(a => a.City)
		//							.ThenInclude(c => c.Country)
		//					.Include(bar => bar.Ratings)
		//					.Where(bar => !bar.IsUnlisted);

		//var sortedBars = SortBars(bars, sortBy, sortOrder);

		//var dtos = sortedBars.Select(bar => _barMapper.CreateBarDTO(bar));

		//var pagedDtos = await PaginatedList<BarDTO>.CreateAsync(dtos, pageNumber, pageSize);

		//return pagedDtos;
		//public async Task<PaginatedList<BarDTO>> GetAllBarsAsync(string searchString = "", string sortBy = "", string sortOrder = "", int pageNumber = 1, int pageSize = 2, bool allowUnlisted = false)
		//{
		//	var sortCriteria = String.Concat(sortBy, '_', sortOrder);

		//	IQueryable<Bar> bars = _context.Bars
		//			.Where(bar => (!bar.IsUnlisted || allowUnlisted))
		//			.Include(bar => bar.Address)
		//				.ThenInclude(a => a.City)
		//					.ThenInclude(c => c.Country)
		//			.Include(bar => bar.Ratings);

		//	switch (sortCriteria)
		//	{
		//		case "rating_desc":
		//			bars = bars.OrderByDescending(b => b.AverageRating).ThenBy(b => b.Name);
		//			break;
		//		case "rating_":
		//			bars = bars.OrderBy(b => b.AverageRating).ThenBy(b => b.Name);
		//			break;
		//		default:
		//			bars = bars.OrderBy(c => c.Name).ThenBy(c => c.Name);
		//			break;
		//	}

		//	var dtos = bars.Select(bar => _barMapper.CreateBarDTO(bar));

		//	if (searchString != "")
		//		dtos = FilterBars(dtos, searchString);

		//	var pagedBars = await PageBars(dtos, pageNumber, pageSize);

		//	return pagedBars;
		public async Task<ICollection<BarDTO>> GetAllBarsAsync(string searchString = "", bool allowUnlisted = false)
		{
			var bars =  _context.Bars
							.Where(bar => !bar.IsUnlisted || allowUnlisted)
							.Include(bar => bar.Address)
								.ThenInclude(a => a.City)
									.ThenInclude(c => c.Country)
							.Include(bar => bar.Ratings)
							.Select(bar => _barMapper.CreateBarDTO(bar));

			var filteredbars = bars;

			if (searchString != "")
				filteredbars = bars.Where(bar => bar.Name.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
											|| bar.Address.CountryName.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
											|| bar.Address.CityName.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
											|| bar.Address.Street.Contains(searchString, StringComparison.InvariantCultureIgnoreCase));

			return await filteredbars.ToListAsync();
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
									 .ToListAsync();

			var topBarsDTO = topBars.Select(bar => _barMapper.CreateBarDTO(bar))
									.OrderByDescending(bar => bar.AverageRating)
									.Take(ammount)
									.ToList();

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
						.FirstOrDefaultAsync(bar => bar.Id == id);

			if (bar == null)
			{
				throw new ArgumentException("The Bar you were looking for was not found!");
			}

			var barDTO = this._barMapper.CreateBarDTO(bar);

			return barDTO;
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
			bar.ImagePath = barDTO.ImagePath;

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
			if (await _context.Bars.FirstOrDefaultAsync(bar => bar.Name == barDTO.Name) != null)
				throw new DbUpdateException("Bar with the same name already exists!");

			if (barDTO.Name == null)
				throw new ArgumentNullException("Name cannot be null!");

			try
			{
				var address = await _addressServices.CreateAddressAsync(barDTO.Address);

				var bar = new Bar
				{
					Name = barDTO.Name,
					Phone = barDTO.Phone,
					ImagePath = barDTO.ImagePath,
					Details = barDTO.Details,
					AddressID = address.Id
				};

				await _context.Bars.AddAsync(bar);
				await _context.SaveChangesAsync();

				//TODO Ntoast notif
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
		public async Task<BarDTO> DeleteBar(Guid id)
		{
			var bar = await _context.Bars
				.FirstOrDefaultAsync(bar => bar.Id == id && bar.IsUnlisted == false) ?? throw new ArgumentNullException();

			bar.IsUnlisted = true;

			_context.Bars.Update(bar);
			await _context.SaveChangesAsync();

			var barDTO = this._barMapper.CreateBarDTO(bar);

			return barDTO;
		}

		/// <summary>
		/// Adds selected cocktail to specified bar.
		/// </summary>
		/// <param name="barId">The Id of the bar the cocktail should be added to.</param>
		/// <param name="cocktailId">The Id of the cocktail that should be added.</param>
		/// <returns></returns>
		public async Task<BarDTO> AddCocktailToBar(Guid barId, Guid cocktailId)
		{
			var bar = await GetBarEntityWithCocktails(barId);
			var cocktail = await GetCocktailEntity(cocktailId);

			bar.Cocktails.Add(new BarCocktail
			{
				BarId = bar.Id,
				Bar = bar,
				CocktailId = cocktail.Id,
				Cocktail = cocktail
			});

			_context.Update(bar);
			_context.SaveChanges();

			var barDTO = await GetBarAsync(barId);

			return barDTO;
		}

		/// <summary>
		/// Removes selected cocktail from specified bar.
		/// </summary>
		/// <param name="barId">The Id of the bar the cocktail should be removed from.</param>
		/// <param name="cocktailId">The Id of the cocktail that should be removed.</param>
		/// <returns></returns>
		public async Task<BarDTO> RemoveCocktailFromBar(Guid barId, Guid cocktailId)
		{
			var bar = await GetBarEntityWithCocktails(barId);
			var cocktail = await GetCocktailEntity(cocktailId);

			bar.Cocktails.Remove(new BarCocktail
			{
				BarId = bar.Id,
				Bar = bar,
				CocktailId = cocktail.Id,
				Cocktail = cocktail
			});

			_context.Update(bar);
			_context.SaveChanges();

			var barDTO = await GetBarAsync(barId);

			return barDTO;
		}

		private async Task<Bar> GetBarEntityWithCocktails(Guid barId)
		{
			return await _context.Bars
				.Include(bar => bar.Cocktails)
				.FirstOrDefaultAsync(bar => bar.Id == barId && bar.IsUnlisted == false) ?? throw new ArgumentNullException();

		}

		private async Task<Cocktail> GetCocktailEntity(Guid cocktailId)
		{
			return await _context.Cocktails
				.FirstOrDefaultAsync(cocktail => cocktail.Id == cocktailId && cocktail.IsUnlisted == false) ?? throw new ArgumentNullException();
		}
		//private IQueryable<BarDTO> SortBars(IQueryable<BarDTO> bars, string sortBy, string sortOrder)
		//{
		//	return sortBy switch
		//	{
		//		"rating" => string.IsNullOrEmpty(sortOrder) ? bars.OrderBy(c => c.AverageRating)
		//															   .ThenBy(c => c.Name) :
		//													  bars.OrderByDescending(c => c.AverageRating)
		//															   .ThenBy(c => c.Name),

		//		_ => string.IsNullOrEmpty(sortOrder) ? bars.OrderBy(c => c.Name) :
		//											   bars.OrderByDescending(c => c.Name),
		//	};
		//}
	}
}
