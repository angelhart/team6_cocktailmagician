using CM.Web.Areas.BarCrawler.Models;
using CM.Web.Areas.Magician.Models;
using CM.Web.Providers.CustomAttributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Models
{
    [BindProperties]
    public class CocktailViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Recipe { get; set; }
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }

        [DisplayName("Rating")]
        public double? AverageRating { get; set; }
        
        [DisplayName("Unlist")]
        public bool IsUnlisted { get; set; }
        
        [DisplayName("What you can find inside:")]
        public ICollection<IngredientViewModel> Ingredients { get; set; } = new List<IngredientViewModel>();
        
        [DisplayName("Where can you have it:")]
        public ICollection<BarViewModel> Bars { get; set; } = new List<BarViewModel>();

        [DisplayName("See what people are saying about it:")]
        public ICollection<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();
    }
}
