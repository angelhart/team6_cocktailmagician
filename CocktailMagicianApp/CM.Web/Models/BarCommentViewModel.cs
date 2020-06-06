using System;
using System.ComponentModel.DataAnnotations;

namespace CM.Web.Models
{
	public class BarCommentViewModel
	{
		public Guid BarId { get; set; }
		public string BarName { get; set; }
		public Guid UserId { get; set; }
		public string UserName { get; set; }

		[MaxLength(500, ErrorMessage = ("Comment must be less than 500 characters long."))]
		public string Text { get; set; }
		public DateTimeOffset CommentedOn { get; set; }
	}
}