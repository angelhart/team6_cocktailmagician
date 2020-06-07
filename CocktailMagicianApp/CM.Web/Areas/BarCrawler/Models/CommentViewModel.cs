﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Areas.BarCrawler.Models
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }
        public Guid EntityId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }

        [Required(ErrorMessage = ("Comment content can't be empty."))]
        [MaxLength(ErrorMessage = ("Comments can't be more than 500 characters long."))]
        public string Text { get; set; }
        public DateTimeOffset CommentedOn { get; set; }
    }
}
