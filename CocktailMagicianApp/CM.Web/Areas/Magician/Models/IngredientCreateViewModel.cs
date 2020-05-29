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
    public class IngredientCreateViewModel
    {
        [Required]
        [MinLength(1, ErrorMessage = ("Ingredient name can't be empty."))]
        public string Name { get; set; }
        [Required(ErrorMessage = ("Image is required"))]
        public IFormFile Image { get; set; }
    }
}
