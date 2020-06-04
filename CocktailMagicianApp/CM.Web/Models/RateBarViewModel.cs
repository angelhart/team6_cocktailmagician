using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Models
{
	public class RateBarViewModel
	{
		public Guid BarId { get; set; }
		public int Score { get; set; }
		public Guid AppUserId { get; set; }
	}
}
