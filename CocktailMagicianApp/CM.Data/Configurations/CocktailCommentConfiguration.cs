using CM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Data.Configurations
{
    public class CocktailCommentConfiguration : IEntityTypeConfiguration<CocktailComment>
    {
        public void Configure(EntityTypeBuilder<CocktailComment> builder)
        {
            builder.HasKey(cc => new { cc.CocktailId, cc.AppUserId });
            builder.Property(cc => cc.Text)
                .IsRequired();
            builder.HasOne(cc => cc.Cocktail)
                .WithMany(c => c.Comments)
                .HasForeignKey(cc => cc.CocktailId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(cc => cc.AppUser)
                .WithMany(au => au.Comments)
                .HasForeignKey(cc => cc.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
