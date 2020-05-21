using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CM.Models
{
	public class Bar
	{
        public Bar()
        {
            Comments = new List<BarComment>();
            Ratings = new List<BarRating>();
            Cocktails = new List<BarCocktail>();
        }

        public Guid Id { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]
        [MinLength(1)]
        [MaxLength(256)]
        public string Name { get; set; }

        public Guid AddressID { get; set; }
        public Address Address { get; set; }

        public string Phone { get; set; }
        public string ImagePath { get; set; }
        public string Details { get; set; }

        public bool IsUnlisted { get; set; }

        public ICollection<BarComment> Comments { get; set; }
        public ICollection<BarCocktail> Cocktails { get; set; }

        public ICollection<BarRating> Ratings { get; set; }
        public double? AverageRating
        {
            get
            {
                if (Ratings.Count > 0)
                    return Ratings.Average(r => r.Score);
                else
                    return null;
            }
        }
    }
}