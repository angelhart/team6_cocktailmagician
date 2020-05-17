using CM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Data.Configurations
{
    public class BarRatingConfiguration : IEntityTypeConfiguration<BarRating>
    {
        public void Configure(EntityTypeBuilder<BarRating> builder)
        {
            builder.HasKey(cc => new { cc.BarId, cc.AppUserId });
        }
    }
}
