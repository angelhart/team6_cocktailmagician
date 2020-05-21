using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs
{
    public class CocktailSearchDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<string> Ingredients { get; set; }
        //TODO: picture
    }
}
