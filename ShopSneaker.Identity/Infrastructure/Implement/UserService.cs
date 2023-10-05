using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopSneaker.Identity.Database;
using ShopSneaker.Identity.Database.Entities;
using ShopSneaker.Identity.Infrastructure.Helper;
using ShopSneaker.Identity.Infrastructure.Interface;
using ShopSneaker.Identity.Model;

namespace ShopSneaker.Identity.Infrastructure.Implement
{
    public class UserService : IUserService
    {
        private readonly int TOKEN_MINUTE_TIME_LIFE = 6000000;
        private readonly UserManager<Users> _userManager;
        private readonly IMapper _mapper;
        private readonly IdentityDbContext _context;

        public UserService(UserManager<Users> userManager, IMapper mapper, IdentityDbContext context)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
        }

        public async Task<LoginVm> LoginByEmail(string email, string password)
        {
            var user = LoadRelated(await _userManager.FindByEmailAsync(email));

            if (user == null)
            {
                throw new ArgumentException("Email or Password is incorrect");
            }

            var checkPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!checkPassword) { throw new ArgumentException("Email or Password is incorrect"); };

            var model = _mapper.Map<LoginVm>(user);
            model.UserRole = user.Roles.Name;
            model.RoleId = user.Roles.Id.ToString();
            var refreshToken = UniqueIDHelper.GenarateRandomString(12);
            user.RefreshToken = refreshToken;

            var userUpdate = UpdateUser(user);

            if (userUpdate)
            {
                model.RefreshToken = refreshToken;
                model.TokenEffectiveDate = DateTime.Now.ToString();
                model.TokenEffectiveTimeStick = DateTime.Now.Ticks.ToString();
                model.AccountTypeId = ((int)1).ToString();

                var result = JwtTokenHelper.GenerateJwtTokenModel(model, TOKEN_MINUTE_TIME_LIFE);
                return result;
            }

            return new LoginVm();


        }

        private bool UpdateUser (Users users)
        {
            if (users == null)
            {
                return false;
            }    
            var user = _context.Users.Where(u => u.Email == users.Email).FirstOrDefault();
            user!.RefreshToken = users.RefreshToken;
            _context.Update(user);
            _context.SaveChanges();
            return true;
        }

        protected virtual Users LoadRelated(Users user)
        {
            try
            {
                if (user != null)
                {
                    _context.UserRoles
                       .Include(k => k.Role)
                       .Where(k => k.UserId == user.Id)
                       .Load();
                }

                return user;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
