using System;

namespace CM.Web.Models
{
	public class BarCommentViewModel
	{
		public Guid BarId { get; set; }
		public string BarName { get; set; }
		public Guid UserId { get; set; }
		public string UserName { get; set; }
		public string Text { get; set; }
		public DateTimeOffset CommentedOn { get; set; }

	}
}