using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CM.Models.BaseClasses;

namespace CM.Services.Providers
{
	public static class Helper<T>
	{
        public static IQueryable<ISortable> SortCollection(IQueryable<ISortable> collection, string sortBy, string sortOrder)
        {
            return sortBy switch
            {
                "rating" => string.IsNullOrEmpty(sortOrder) ? collection.OrderBy(c => c.AverageRating)
                                                                       .ThenBy(c => c.Name) :
                                                              collection.OrderByDescending(c => c.AverageRating)
                                                                       .ThenBy(c => c.Name),

                _ => string.IsNullOrEmpty(sortOrder) ? collection.OrderBy(c => c.Name) :
                                                       collection.OrderByDescending(c => c.Name),
            };
        }
    }
}
