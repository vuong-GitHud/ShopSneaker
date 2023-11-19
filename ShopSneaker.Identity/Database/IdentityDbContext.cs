using Microsoft.EntityFrameworkCore;
using ShopSneaker.Identity.Database.Entities;

namespace ShopSneaker.Identity.Database
{
    public class IdentityDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<RoleControls> RoleControls { get; set; }

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
            //DataMigration();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-DHG9R36;Database=Identity;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(k => k.Id);
                entity.Property(k => k.Id)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

                entity.Property(k => k.UserId)
                .HasColumnName("UserId")
                .HasColumnType("uniqueidentifier");

                entity.Property(k => k.FirstName)
                .HasColumnName("FirstName")
                .HasMaxLength(20);

                entity.Property(k => k.Street)
                .HasColumnName("Street")
                .HasMaxLength(40);

                entity.Property(k => k.City)
                .HasColumnName("City")
                .HasMaxLength(20);

                entity.Property(k => k.State)
                .HasColumnName("State")
                .HasMaxLength(20);

                entity.Property(k => k.Country)
                .HasColumnName("Country")
                .HasMaxLength(20);

                entity.Property(k => k.IsActivated)
                .HasColumnName("IsActivated");

                entity.Property(k => k.RefreshToken)
                .HasColumnName("RefreshToken")
                .HasMaxLength(12);

                entity.Property(k => k.CreatedBy)
                .HasColumnName("CreatedBy")
                .HasColumnType("uniqueidentifier");

                entity.Property(k => k.CreatedDate)
                .HasColumnName("CreatedDate")
                .HasColumnType("datetime");

                entity.Property(k => k.TokenEffectiveDate)
                .HasColumnName("TokenEffectiveDate")
                .HasColumnType("datetime");

                entity.Property(k => k.TokenEffectiveTimeStick)
                .HasColumnName("TokenEffectiveTimeStick")
                .HasColumnType("bigint");

                entity.Property(k => k.DOB)
               .HasColumnName("Birthday")
               .HasColumnType("datetime");

                entity.Property(k => k.PhoneNumber)
                .HasColumnName("PhoneNumber")
                .HasMaxLength(12);

                entity.Ignore(k => k.Roles);
            });

            modelBuilder.Entity<Roles>(entity => {
                entity.ToTable("Roles");
                entity.Ignore(k => k.SubordinateRoles);
                entity.Property(k => k.Id).ValueGeneratedOnAdd();
                entity.HasKey(k => k.Id);
                entity.Property(k => k.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<UserRoles>(entity => {
                entity.ToTable("UserRoles");
                entity.HasKey(k => new { k.UserId, k.RoleId });
                entity.HasOne(k => k.User).WithMany(k => k.UserRoles).HasForeignKey(k => k.UserId);
                entity.HasOne(k => k.Role).WithMany(k => k.UserRoles).HasForeignKey(k => k.RoleId);
            });

            modelBuilder.Entity<RoleControls>(entity => {
                entity.ToTable("RoleControls");
                entity.HasKey(k => new { k.SuperiorFid, k.SubordinateFid });
                entity.Property(k => k.SuperiorFid).HasColumnName("SuperiorFid");
                entity.Property(k => k.SubordinateFid).HasColumnName("SubordinateFid");
                entity.HasOne(k => k.SubordinateRole).WithMany(k => k.SubordinateRoles).HasForeignKey(k => k.SubordinateFid);
            });

            modelBuilder.Seeds();
        }
        private void DataMigration()
        {
            if (Roles.CountAsync().Result == 0)
            {
                var roles = new List<Roles>() {
                    new Roles(){
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                    },
                    new Roles(){
                        Name = "Customer",
                        NormalizedName = "CUSTOMER",
                    },
                };
                Roles.AddRange(roles);
                SaveChanges();
            }
            if (RoleControls.CountAsync().Result == 0)
            {
                var rolecontrolls = new List<RoleControls>() {
                    new RoleControls(){
                        SuperiorFid = 1,
                        SubordinateFid = 2
                    },
                    new RoleControls(){
                        SuperiorFid = 1,
                        SubordinateFid = 3
                    },
                    new RoleControls(){
                        SuperiorFid = 1,
                        SubordinateFid = 4
                    },
                    new RoleControls(){
                        SuperiorFid = 1,
                        SubordinateFid = 5
                    },new RoleControls(){
                        SuperiorFid = 1,
                        SubordinateFid = 6
                    },new RoleControls(){
                        SuperiorFid = 1,
                        SubordinateFid = 7
                    },
                    new RoleControls(){
                        SuperiorFid = 1,
                        SubordinateFid = 8
                    },
                    new RoleControls(){
                        SuperiorFid = 5,
                        SubordinateFid = 3
                    },
                    new RoleControls(){
                        SuperiorFid = 5,
                        SubordinateFid = 4
                    },
                    new RoleControls(){
                        SuperiorFid = 5,
                        SubordinateFid = 6
                    },
                    new RoleControls(){
                        SuperiorFid = 5,
                        SubordinateFid = 7
                    },
                    new RoleControls(){
                        SuperiorFid = 5,
                        SubordinateFid = 8
                    },
                    new RoleControls(){
                        SuperiorFid = 3,
                        SubordinateFid = 4
                    },
                    new RoleControls(){
                        SuperiorFid = 3,
                        SubordinateFid = 5
                    },
                    new RoleControls(){
                        SuperiorFid = 3,
                        SubordinateFid = 6
                    },
                    new RoleControls(){
                        SuperiorFid = 3,
                        SubordinateFid = 7
                    },

                };
                RoleControls.AddRange(rolecontrolls);
                base.SaveChanges();
            }
        }
    }
}
