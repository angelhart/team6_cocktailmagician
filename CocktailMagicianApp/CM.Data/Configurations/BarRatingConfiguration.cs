using CM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CM.Data.Configurations
{
	public class BarRatingConfiguration : IEntityTypeConfiguration<BarRating>
    {
        public void Configure(EntityTypeBuilder<BarRating> builder)
        {
            builder.HasKey(barRating => new { barRating.BarId, barRating.AppUserId });
            builder.Property(barRating => barRating.Score)
                .IsRequired();
            builder.HasOne(barRating => barRating.Bar)
                .WithMany(b => b.Ratings)
                .HasForeignKey(barRating => barRating.BarId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(barRating => barRating.AppUser)
                .WithMany(appUser => appUser.BarRatings)
                .HasForeignKey(barRating => barRating.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
