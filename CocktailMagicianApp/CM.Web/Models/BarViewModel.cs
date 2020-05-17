using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Models
{
	public class BarViewModel: BarIndexViewModel
    {
        public string Street { get; set; }
        public string Phone { get; set; }
        public string Details { get; set; }
        public ICollection<BarCommentViewModel> Comments { get; set; }
        public ICollection<BarCocktailViewModel> Cocktails { get; set; }
    }
}
