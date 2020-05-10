using System;
using System.Collections.Generic;

namespace CM.Models
{
	public class Country
	{
		public Guid Id { get; set; }
		public string Name { get; set; }

		public ICollection<Address> Adresses { get; set; }
}
}