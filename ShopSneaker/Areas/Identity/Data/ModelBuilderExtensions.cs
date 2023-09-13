using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopSneaker.Data;
using ShopSneaker.Data.Entities;

namespace ShopSneaker.Areas.Identity.Data;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    Name = "Shoes",
                    Description = "This Is Shoes",
                },
                new Category()
                {
                    Id = 2,
                    Name = "Sandal",
                    Description = "This Is Sandal",
                });
            modelBuilder.Entity<Product>().HasData(
           new Product()
           {
               Id = 1,
               Name = "Jordan1",
               CreateDate = DateTime.Now,
               Price = 100000,
               Description = "This is Jordan1",
               Rating = 5,
               CategoryId = 1,
               ThumbPath = "asd"
           },
            new Product()
            {
                Id = 2,
                Name = "Jordan 2",
                CreateDate = DateTime.Now,
                Price = 20000,
                Description = "This is Jordan 2",
                Rating = 10,
                CategoryId = 2,
                ThumbPath = "asd",
            });

            var roleId = new Guid("7225DA6B-65FC-4B04-8F46-FD3176512EFF");  /*< Guid("7225DA6B-65FC-4B04-8F46-FD3176512EFF") >*/
            var adminId = new Guid("D60A3A17-4053-42BB-A858-F44E7825BDF4");  /*< Guid("D60A3A17-4053-42BB-A858-F44E7825BDF4") >*/
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Admin Role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "admin.vuong@gmail.com",
                NormalizedEmail = "admin.vuong@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Vuong@123"),
                SecurityStamp = string.Empty,
                FullName = "Pham Xuan Vuong",
                DOB = new DateTime(2000, 01, 01)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
}
    