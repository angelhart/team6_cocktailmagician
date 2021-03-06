﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CM.Models.BaseClasses
{
	public class Comment
	{
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        [Required(ErrorMessage = "Comment content must not be empty")]
        [MinLength(1)]
        [MaxLength(500)]
        public string Text { get; set; }
        public DateTimeOffset CommentedOn { get; set; }
    }
}
