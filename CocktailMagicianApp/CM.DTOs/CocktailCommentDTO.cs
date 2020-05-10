using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace CM.DTOs
{
    public class CocktailCommentDTO
    {
        public Guid CocktailId { get; set; }
        public Guid AppUserId { get; set; }
        public string Text { get; set; }
        public DateTimeOffset CommentedOn { get; set; }
    }
}