using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers.Contracts;
using CM.Models;
using CM.Services.Contracts;
using CM.Services.Providers.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services
{
	public class BarServices : IBarServices
	{
		private readonly CMContext _context;
		private readonly IBarMapper _barMapper;
		private readonly IAddressMapper _addressMapper;

		public BarServices(CMContext context, IBarMapper barMapper, IAddressMapper addressMapper)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_barMapper = barMapper ?? throw new ArgumentNullException(nameof(barMapper));
			_addressMapper = addressMapper ?? throw new ArgumentNullException(nameof(addressMapper));
		}


		/// <summary>
		/// Retrieves a collection of all bars in the database.
		/// </summary>
		/// <returns>ICollection</returns>
		public async Task<ICollection<BarDTO>> GetAllBarsAsync()
		{

			var bars = await _context.Bars
						.Include(a => a.Address)
							.ThenInclude(a => a.City)
								.ThenInclude(c => c.Country)
						.Include(c => c.Cocktails)
						.Where(bar => bar.IsUnlisted == false)
						.OrderBy(bar => bar.Name)
						.Select(bar => this._barMapper.CreateBarDTO(bar))
						.ToListAsync();

			return bars;
		}

		/// <summary>
		/// Retrieves a specific bar by given Id.
		/// </summary>
		/// <param name="id">Guid representing Id.</param>
		/// <returns>BarDTO</returns>
		public async Task<BarDTO> GetBarAsync(Guid id)
		{
			var bar = await _context.Bars
						.Include(a => a.Address)
							.ThenInclude(a => a.City)
								.ThenInclude(c => c.Country)
						.Include(c => c.Cocktails)
							.ThenInclude(c => c.Cocktail)
						.FirstOrDefaultAsync(b => b.Id == id);

			if (bar == null)
			{
				throw new ArgumentNullException();
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
						.FirstOrDefaultAsync(b => b.Id == id);

			if (bar == null)
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
				throw new ArgumentException("Bar with the same name already exists!");

			try
			{
				var address = _addressMapper.CreateAddress(barDTO);
				var bar = _barMapper.CreateBar(barDTO, address);

				await _context.Bars.AddAsync(bar);
				await _context.Addresses.AddAsync(address);
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

			bar.Cocktails.Add(_barMapper.CreateBarCocktail(bar, cocktail));

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

			bar.Cocktails.Remove(_barMapper.CreateBarCocktail(bar, cocktail));

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
	}//TODO Search,  top5 bars
	 //TODO sorting
	 //TODO paging
}
