using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CM.Web.Models
{
    public class CocktailMenuViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [DisplayName("Image")]
        public string ImagePath { get; set; }
        [DisplayName("Price")]
        public ICollection<BarViewModel> BarPrices { get; set; }
    }
}
