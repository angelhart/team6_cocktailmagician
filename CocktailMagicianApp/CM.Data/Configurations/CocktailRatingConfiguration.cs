using CM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Data.Configurations
{
    public class CocktailRatingConfiguration : IEntityTypeConfiguration<CocktailRating>
    {
        public void Configure(EntityTypeBuilder<CocktailRating> builder)
        {
            builder.HasKey(cr => new { cr.CocktailId, cr.AppUserId });
            builder.Property(cr => cr.Score)
                .IsRequired();
            builder.HasOne(cr => cr.Cocktail)
                .WithMany(c => c.Ratings)
                .HasForeignKey(cr => cr.CocktailId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(cr => cr.AppUser)
                .WithMany(au => au.CocktailRatings)
                .HasForeignKey(cr => cr.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
