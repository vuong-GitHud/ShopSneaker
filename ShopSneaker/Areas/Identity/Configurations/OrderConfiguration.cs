using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopSneaker.Data.Entities;

namespace ShopSneaker.Areas.Identity.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.OrderDate);
        }
    }
}
