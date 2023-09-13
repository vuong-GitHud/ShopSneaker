using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopSneaker.Data.Entities;

namespace ShopSneaker.Areas.Identity.Configurations
{
	public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
	{
		public void Configure(EntityTypeBuilder<AppRole> builder)
		{
			builder.ToTable("AppRoles");

			builder.Property(x => x.Description).HasMaxLength(200).IsRequired();

		}
	}
}
