using CM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CM.Data.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(address => address.Id);
            builder.HasOne(address => address.Bar)
                .WithOne(bar => bar.Address)
                .HasForeignKey<Address>(address => address.BarId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
