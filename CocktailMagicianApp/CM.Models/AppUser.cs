using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CM.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public ICollection<CocktailRating> Ratings { get; set; }
        public ICollection<CocktailComment> Comments { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
        //TODO Image?
    }
}