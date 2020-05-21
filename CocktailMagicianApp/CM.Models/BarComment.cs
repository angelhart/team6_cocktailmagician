using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CM.Models.BaseClasses;

namespace CM.Models
{
	public class BarComment: Comment
    {
        public Guid BarId { get; set; }
        public Bar Bar { get; set; }
    }
}
