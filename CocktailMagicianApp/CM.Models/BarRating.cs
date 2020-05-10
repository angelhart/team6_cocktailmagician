using System;
using System.Collections.Generic;
using System.Text;
using CM.Models.BaseClasses;

namespace CM.Models
{
	public class BarRating: Rating
	{
		public Guid BarId { get; set; }
		public Bar Bar { get; set; }
	}
}
