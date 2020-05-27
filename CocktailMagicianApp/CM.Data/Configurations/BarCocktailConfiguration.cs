using CM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Data.Configurations
{
    public class BarCocktailConfiguration : IEntityTypeConfiguration<BarCocktail>
    {
        public void Configure(EntityTypeBuilder<BarCocktail> builder)
        {
            builder.HasKey(bc => new { bc.CocktailId, bc.BarId });
            builder.HasOne(bc => bc.Cocktail)
                .WithMany(c => c.Bars)
                .HasForeignKey(bc => bc.CocktailId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(bc => bc.Bar)
                .WithMany(b => b.Cocktails)
                .HasForeignKey(bc => bc.BarId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
