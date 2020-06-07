﻿using CM.Web.Areas.BarCrawler.Models;
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

        [Required(ErrorMessage = ("Name is required."))]
        [MinLength(2, ErrorMessage = ("Name must be more than 2 characters long."))]
        [MaxLength(50, ErrorMessage = ("Name must be less than 50 characters long."))]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = ("Recipe must be less than 500 characters long."))]
        public string Recipe { get; set; }

        [AllowedExtensions(new string[] { ".jpg", ".png", ".gif" }, ErrorMessage = ("Only .png, .jpg or .gif files allowed!"))]
        [MaxSize(1 * 1024 * 1024, ErrorMessage = ("Size must be less than 1 MB!"))]
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
