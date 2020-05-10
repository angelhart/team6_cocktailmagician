using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CM.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public ICollection<BarRating> BarRatings { get; set; }
        public ICollection<BarComment> BarComments { get; set; }

        public ICollection<CocktailRating> CocktailRatings { get; set; }
        public ICollection<CocktailComment> CocktailComments { get; set; }
        
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
        //TODO Image?
    }
}