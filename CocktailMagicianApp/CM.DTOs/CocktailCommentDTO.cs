using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace CM.DTOs
{
    public class CocktailCommentDTO
    {
        public Guid Id { get; set; }
        public Guid CocktailId { get; set; }
        public string CocktailName { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public DateTimeOffset CommentedOn { get; set; }
    }
}