using System;

namespace CM.DTOs
{
    public class BarPriceDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public bool IsUnlisted { get; set; }
        public float Price { get; set; }
    }
}