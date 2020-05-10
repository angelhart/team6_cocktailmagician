using CM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Data.Configurations
{
    public class CocktailIngredientConfiguration : IEntityTypeConfiguration<CocktailIngredient>
    {
        public void Configure(EntityTypeBuilder<CocktailIngredient> builder)
        {
            builder.HasKey(ci => new { ci.CocktailId, ci.IngredientId });
            builder.HasOne(ci => ci.Cocktail)
                .WithMany(c => c.Ingredients)
                .HasForeignKey(ci => ci.CocktailId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(ci => ci.Ingredient)
                .WithMany(i => i.Cocktails)
                .HasForeignKey(ci => ci.IngredientId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(ci => ci.Unit)
                .HasConversion(
                    u => u.ToString(), 
                    u => (Unit)Enum.Parse(typeof(Unit), u));
        }
    }
}
