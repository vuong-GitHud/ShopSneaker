using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopSneaker.Identity.Database.Entities;

namespace ShopSneaker.Identity.Database
{
    public static class Seed
    {
        public static void Seeds(this ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<Users>();
            modelBuilder.Entity<Users>().HasData(new Users("vuong@gmail.com", "admin", "admin", "admin")
            {
                Id = 1,
                UserId = new Guid("FE6EEC2B-239B-4CB6-AEB1-25106220C7F0"),
                UserName = "vuong@gmail.com",
                NormalizedUserName = "admin",
                NormalizedEmail = "vuong@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Vuong@123"),
                SecurityStamp = string.Empty,
                DOB = new DateTime(2000, 06, 28),
                City = "HCM",
                Country = "HCM",
                State = "",
                Street = "",
                RefreshToken = "",
                CreatedDate = DateTime.Now,
                FullName = "admin",
                Ward = "Phường 13"
            });
        }
    }
}
