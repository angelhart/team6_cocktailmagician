using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Areas.BarCrawler.Models
{
    public class RatingViewModel
    {
        [Required(ErrorMessage = ("No item ID provided."))]
        public Guid EntityId { get; set; }

        [Required(ErrorMessage = ("No user ID provided."))]
        public Guid UserId { get; set; }
        public string UserName { get; set; }

        [Required(ErrorMessage = ("Rating must have a score from 1 to 5."))]
        [Range(1, 5, ErrorMessage = ("Rating must have a score from 1 to 5."))]
        public int Score { get; set; }
    }
}
