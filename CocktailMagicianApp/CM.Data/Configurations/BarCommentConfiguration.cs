using CM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CM.Data.Configurations
{
	public class BarCommentConfiguration: IEntityTypeConfiguration<BarComment>
	{
        public void Configure(EntityTypeBuilder<BarComment> builder)
        {
            builder.HasKey(barComment => new { barComment.BarId, barComment.AppUserId });
            builder.HasOne(barComment => barComment.Bar)
                .WithMany(c => c.Comments)
                .HasForeignKey(barComment => barComment.BarId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(barComment => barComment.AppUser)
                .WithMany(appUser => appUser.BarComments)
                .HasForeignKey(barComment => barComment.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
