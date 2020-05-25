using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Models.BaseClasses
{
	public interface ISortable
	{
		public string Name { get; set; }
		public double? AverageRating { get; }
	}
}
