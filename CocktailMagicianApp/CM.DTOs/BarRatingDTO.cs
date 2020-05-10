using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs
{
	public class BarRatingDTO
	{
		public Guid BarId { get; set; }
		public string BarName { get; set; }
		public int Score { get; set; }
		public Guid AppUserId { get; set; }
		public string AppUserName { get; set; }
	}
}
