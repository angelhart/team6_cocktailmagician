using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CM.Models.BaseClasses
{
	public class Comment
	{
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(500)]
        public string Text { get; set; }
        public DateTimeOffset CommentedOn { get; set; }
    }
}
