using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CM.Models.BaseClasses
{
	public class Rating
	{
		public Guid AppUserId { get; set; }
		public AppUser AppUser { get; set; }
		[Required(ErrorMessage = "Rating must be an integer in the range 1 to 5.")]
		[Range(1, 5)]
		public int Score { get; set; }
	}
}
