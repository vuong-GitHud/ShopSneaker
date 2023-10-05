using Microsoft.AspNetCore.Identity;
using System.Data;

namespace ShopSneaker.Identity.Database.Entities
{
    public class Users : IdentityUser<int>
    {
        public Guid UserId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string FullName { get; set; }

        public DateTime? DOB { get; set; }

        public string? Street { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Ward { get; set; }

        public string? Country { get; set; }

        public string? PhoneNumber { get; set; }

        public bool IsActivated { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? TokenEffectiveDate { get; set; }

        public long? TokenEffectiveTimeStick { get; set; }

        public string? RefreshToken { get; set; }

        public virtual List<UserRoles> UserRoles { get; set; }

        public virtual Roles Roles
        {
            get
            {
                if (UserRoles == null || UserRoles.Count == 0)
                    return new Roles() { Id = 0, Name = string.Empty };

                return UserRoles.Select(k => k.Role).FirstOrDefault();
            }
        }

        public Users(string email, string firstName, string lastName, string fullName) : base()
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            FullName = fullName;
            InitCommonProperties();
        }

        private void InitCommonProperties()
        {
            UserId = Guid.NewGuid();
            NormalizedEmail = Email?.ToUpper();
            UserName = Email;
            NormalizedUserName = Email?.ToUpper();
            CreatedBy = UserId;
            CreatedDate = DateTime.Now;
            IsActivated = true;
            SecurityStamp = Guid.NewGuid().ToString("D");
            LockoutEnabled = true;
            DOB = null;
            TokenEffectiveDate = DateTime.Now;
            TokenEffectiveTimeStick = 0;
            TokenEffectiveDate = CreatedDate;
        }
    }
}
