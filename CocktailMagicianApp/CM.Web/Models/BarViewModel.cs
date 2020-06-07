using System;
using System.Collections.Generic;
using CM.Web.Providers.CustomAttributes;
using Microsoft.AspNetCore.Http;

namespace CM.Web.Models
{
	public class BarViewModel: BarIndexViewModel
    {
        public string Phone { get; set; }
        public string Details { get; set; }
        public int? Score { get; set; }
        public Guid AddressId { get; set; }

        [AllowedExtensions(new string[] { ".jpg", ".png", ".gif" }, ErrorMessage = ("Only .png, .jpg or .gif files allowed!"))]
        [MaxSize(1 * 1024 * 1024, ErrorMessage = ("Size must be less than 1 MB!"))]
        public IFormFile Image { get; set; }

        public ICollection<BarCommentViewModel> Comments { get; set; }
        public ICollection<CocktailViewModel> Cocktails { get; set; }
        public Guid [] SelectedCocktails { get; set; }
    }
}
