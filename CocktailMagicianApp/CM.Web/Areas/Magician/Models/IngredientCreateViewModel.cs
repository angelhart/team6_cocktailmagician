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
    //[RequestSizeLimit(1048576)] // 1 Megabyte = 1024 bytes * 1024 kylobytes
    public class IngredientCreateViewModel
    {
        [Required(ErrorMessage = ("Ingredient name can't be empty."))]
        [MinLength(2, ErrorMessage = ("More than one symbol expected."))]
        [Display(Name = "Ingredient Name")]
        public string Name { get; set; }

        [AllowedExtensions(new string[] { ".jpg", ".png" }, ErrorMessage = ("Only .jpg and .png allowed!"))]
        [MaxFileSize(1 * 1024 * 1024, ErrorMessage = ("Size must be less than 1 MB!"))]
        [Required(ErrorMessage = ("Provide an image for the ingredient."))]
        [Display(Name = "Ingredient Image")]
        public IFormFile Image { get; set; }
    }
}
