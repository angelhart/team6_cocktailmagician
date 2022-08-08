//using CM.Data;
//using CM.Services.Contracts;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace CM.Services
//{
//    internal class MenuServices
//    {
//        private readonly CMContext _context;
//        private readonly IBarServices _barServices;
//        private readonly ICocktailServices _cocktailServices;

//        public MenuServices(CMContext context, IBarServices barServices, ICocktailServices cocktailServices)
//        {
//            _context = context ?? throw new ArgumentNullException(nameof(context));
//            _barServices = barServices;
//            _cocktailServices = cocktailServices;
//        }

//        public async Task<PaginatedList<BarDTO>> GetBarsWithCocktailsAsync(string searchString = "", int pageNumber = 1, int pageSize = 10,
//                                                                string sortBy = "", string sortOrder = "", bool allowUnlisted = false)
//        {
//            var bars = await _barServices.GetAllBarsAsync(searchString, pageNumber, pageSize, sortBy, sortOrder, allowUnlisted);

//            return bars;
            
//            bars = _context.Bars
//                            .Include(bar => bar.Cocktails)
//                                .ThenInclude(c => c.Cocktail)
//                            .Where(bar => !bar.IsUnlisted || allowUnlisted);

//            if (!String.IsNullOrEmpty(searchString))
//            {
//                if (Double.TryParse(searchString, out double number))
//                {
//                    bars = bars.Where(bar => bar.AverageRating.Equals(number));
//                }
//                else
//                {
//                    bars = bars.Where(bar => bar.Name.Contains(searchString));
//                }
//            }

//            bars = _barServices.SortBarsAsync(bars, sortBy, sortOrder);

//            var filteredBarsCount = await bars.CountAsync();

//            var filteredBarsList = await bars.Skip((pageNumber - 1) * pageSize)
//                                             .Take(pageSize)
//                                             .ToListAsync();

//            var dtos = filteredBarsList.Select(bar => _barMapper.CreateBarDTO(bar)).ToList();

//            var pagedDtos = await PaginatedList<BarDTO>.CreateAsync(dtos, pageNumber, pageSize);

//            pagedDtos.SourceItems = filteredBarsCount;

//            return pagedDtos;
//        }
//    }
//}
