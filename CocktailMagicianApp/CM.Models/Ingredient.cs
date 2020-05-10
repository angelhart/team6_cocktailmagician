using System;
using System.Collections;
using System.Collections.Generic;

namespace CM.Models
{
    public class Ingredient
    {
        public Ingredient()
        {
            Cocktails = new List<CocktailIngredient>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<CocktailIngredient> Cocktails { get; set; }
    }
}