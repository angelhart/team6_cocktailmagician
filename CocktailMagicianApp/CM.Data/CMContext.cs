using CM.Data.Configurations;
using CM.Data.Seeder;
using CM.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace CM.Data
{
    public class CMContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public CMContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Bar> Bars { get; set; }
        public DbSet<BarRating> BarRatings { get; set; }
        public DbSet<BarComment> BarComments { get; set; }
        public DbSet<BarCocktail> BarCocktails { get; set; }

        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<CocktailRating> CocktailRatings { get; set; }
        public DbSet<CocktailComment> CocktailComments { get; set; }
        public DbSet<CocktailIngredient> CocktailIngredients { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AddressConfiguration());
            builder.ApplyConfiguration(new CityConfiguration());

            builder.ApplyConfiguration(new BarCocktailConfiguration());
            builder.ApplyConfiguration(new BarCommentConfiguration());
            builder.ApplyConfiguration(new BarRatingConfiguration());

            builder.ApplyConfiguration(new CocktailIngredientConfiguration());
            builder.ApplyConfiguration(new CocktailCommentConfiguration());
            builder.ApplyConfiguration(new CocktailRatingConfiguration());

			builder.Seed();

			base.OnModelCreating(builder);
        }
    }
}
