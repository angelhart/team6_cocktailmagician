using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CM.Models;

namespace CM.DTOs
{
	public class BarDTO
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public ICollection<BarCommentDTO> Comments { get; set; }
        public double? AverageRating { get; set; }
        public ICollection<BarCocktailDTO> Cocktails { get; set; }
        public string ImagePath { get; set; }
    }
}
