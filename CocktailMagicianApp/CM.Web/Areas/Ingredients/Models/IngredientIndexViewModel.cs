using CM.Services.Providers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Areas.Ingredients.Models
{

    [BindProperties]
    public class IngredientIndexViewModel
    {
        public string SearchString { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public ICollection<IngredientViewModel> PageIngredients { get; set; }
    }
}
