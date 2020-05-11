using System;

namespace CM.DTOs
{
    public class CocktailHomePageDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        // TODO: picture
        public double Rating { get; set; }
    }
}