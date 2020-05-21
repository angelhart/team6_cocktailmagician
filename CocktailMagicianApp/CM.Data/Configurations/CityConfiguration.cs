using System;
using System.Collections.Generic;
using System.Text;
using CM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CM.Data.Configurations
{
	public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(city => city.Id);
            builder.HasOne(city => city.Country)
                .WithMany(country => country.Cities);
            builder.HasMany(city => city.Adresses);
        }
    }
}
