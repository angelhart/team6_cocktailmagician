using CM.Models;
using CM.Web.Models;
using CM.Web.Providers.CustomAttributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Areas.Magician.Models
{
    [BindProperties]
    public class IngredientViewModel
    {
        public IngredientViewModel()
        {
            Cocktails = new List<CocktailViewModel>();
        }
        public Guid Id { get; set; }

        [Required(ErrorMessage = ("Ingredient name can't be empty."))]
        [MinLength(2, ErrorMessage = ("More than one symbol expected."))]
        [MaxLength(50, ErrorMessage = ("Name should be less than 50 symbols."))]
        public string Name { get; set; }

        //[Required(ErrorMessage = ("Provide an image for the ingredient."))]
        //[RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(\.png|\.jpg|\.gif)$", ErrorMessage = "Only .png, .jpg or .gif files allowed.")]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".gif" }, ErrorMessage = ("Only .png, .jpg or .gif files allowed!"))]
        [MaxSize(1 * 1024 * 1024, ErrorMessage = ("Size must be less than 1 MB!"))]
        public IFormFile Image { get; set; }

        public string ImagePath { get; set; }
        public ICollection<CocktailViewModel> Cocktails { get; set; }
    }
}
