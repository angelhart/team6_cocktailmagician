using System;
using System.Collections.Generic;
using System.Text;
using CM.Models;

namespace CM.DTOs
{
	public class AppUserDTO
	{
		public Guid Id { get; set; }
		public string Username { get; set; }

		public ICollection<BarRatingDTO> BarRatings { get; set; }
		public ICollection<BarCommentDTO> BarComments { get; set; }

		public ICollection<CocktailRatingDTO> CoctailRatings { get; set; }
		public ICollection<CocktailCommentDTO> CocktailComments { get; set; }

		public DateTime? DeletedOn { get; set; }
		public bool IsDeleted { get; set; }
		public string ImagePath { get; set; }
	}
}
