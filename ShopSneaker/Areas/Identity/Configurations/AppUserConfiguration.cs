using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopSneaker.Data.Entities;

namespace ShopSneaker.Areas.Identity.Configurations
{
	public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
	{
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.DOB).IsRequired();

        }
    }
}
