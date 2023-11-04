using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopSneaker.Identity.Database;
using ShopSneaker.Identity.Database.Entities;
using ShopSneaker.Identity.Infrastructure.Interface;
using ShopSneaker.Identity.Model;

namespace ShopSneaker.Identity.Infrastructure.Implement;

public class AccountService : IAccountService
{
    protected UserManager<Users> _userManager;
    protected readonly IdentityDbContext _db;
    private readonly IMapper _mapper;

    public AccountService(UserManager<Users> userManager, IdentityDbContext db, IMapper mapper)
    {
        _userManager = userManager;
        _db = db;
        _mapper = mapper;
    }

    public async Task<bool> CheckEmailExist(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null);
        {
            return false;
        }
        return true;
    }

    public async Task<UserProfileViewModel> GetUserProfile(string userId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserId.ToString().ToLower() == userId.ToLower());

        if (user == null)
        {
            return new UserProfileViewModel();    
        }
        
        _db.UserRoles.Include(k => k.Role)
            .Where(k => k.UserId == user.Id)
            .Load();
            
        var model = _mapper.Map<Users, UserProfileViewModel>(user);
        return model;
    }
}