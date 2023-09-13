using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopSneaker.Data.Entities;

namespace ShopSneaker.Areas.Identity.Configurations
{
	public class ProductImgConfiguration : IEntityTypeConfiguration<ProductImg>
	{
        public void Configure(EntityTypeBuilder<ProductImg> builder)
        {
            builder.ToTable("ProductImgs");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.ImgPath).HasMaxLength(200).IsRequired(true);
            builder.HasOne(x => x.Product).WithMany(x => x.ProductImgs).HasForeignKey(x => x.ProductId);

        }
    }
}
