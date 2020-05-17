using CM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Data.Configurations
{
    public class BarCommentConfiguration : IEntityTypeConfiguration<BarComment>
    {
        public void Configure(EntityTypeBuilder<BarComment> builder)
        {
            builder.HasKey(cc => new { cc.BarId, cc.AppUserId });
        }
    }
}
