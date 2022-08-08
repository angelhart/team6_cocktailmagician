using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CM.Web.Models
{
    public class BarMenuViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = ("Name is required."))]
        public string Name { get; set; }
        public ICollection<CocktailViewModel> Cocktails { get; set; }
    }
}
