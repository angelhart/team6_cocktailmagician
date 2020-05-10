using CM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Data.Configurations
{
    public class CocktailConfiguration : IEntityTypeConfiguration<Cocktail>
    {
        public void Configure(EntityTypeBuilder<Cocktail> builder)
        {
            builder.Ignore(c => c.AverageRating);
        }
    }
}
