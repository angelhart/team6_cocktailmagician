using CM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CM.Data.Configurations
{
	public class BarConfiguration : IEntityTypeConfiguration<Bar>
	{
		public void Configure(EntityTypeBuilder<Bar> builder)
		{
			builder.Ignore(c => c.AverageRating);
		}
	}
}
