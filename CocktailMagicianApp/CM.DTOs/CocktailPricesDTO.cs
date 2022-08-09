using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs
{
    public class CocktailPricesDTO
    {
        public CocktailPricesDTO()
        {
            BarPrices = new List<BarPriceDTO>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public ICollection<BarPriceDTO> BarPrices { get; set; }
    }
}
