using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers.Contracts;
using CM.Models;
using CM.Services.Contracts;
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
        public async Task<ICollection<BarDTO>> GetAllBars()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves a specific bar by given Id.
        /// </summary>
        /// <param name="id">Guid representing Id.</param>
        /// <returns>BarDTO</returns>
        public async Task<BarDTO> GetBar(Guid id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Updates the current state of a bar.
        /// </summary>
        /// <param name="id">Guid representing Id.</param>
        /// <param name="barDTO">The Bar that should be updated.</param>
        /// <returns></returns>
        public async Task<BarDTO> UpdateBar(Guid id, BarDTO barDTO)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new Bar.
        /// </summary>
        /// <param name="barDTO">The params needed to create a bar. </param>
        /// <returns>BarDTO</returns>
        public async Task<BarDTO> CreateBar(BarDTO barDTO)
        {
            throw new NotImplementedException();
        }

        private async Task<IQueryable<Bar>> SearchBarsAsync(string searchString)
        {
            throw new NotImplementedException();
        }
        private async Task<IQueryable<Bar>> SortBarsAsync(string sortBy, string sortOrder)
        {
            throw new NotImplementedException();
        }
    }
}
