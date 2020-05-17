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
	public class BarServices : IBarServices
	{
		private readonly CMContext _context;
		private readonly IBarMapper _barMapper;

		public BarServices(CMContext context, IBarMapper barMapper)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_barMapper = barMapper ?? throw new ArgumentNullException(nameof(barMapper));
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
						//.Where(bar => bar.IsUnlisted == false)
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
			throw new NotImplementedException();
		}

	}
}
