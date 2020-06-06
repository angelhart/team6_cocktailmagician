using System;
using System.Collections.Generic;

namespace CM.Web.Models
{
	public class BarViewModel: BarIndexViewModel
    {
        public string Phone { get; set; }
        public string Details { get; set; }
        public int? Score { get; set; }
        public ICollection<BarCommentViewModel> Comments { get; set; }
        public ICollection<CocktailViewModel> Cocktails { get; set; }
        public Guid [] SelectedCocktails { get; set; }
    }
}
