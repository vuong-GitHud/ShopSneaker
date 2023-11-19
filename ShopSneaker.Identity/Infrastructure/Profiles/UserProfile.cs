using AutoMapper;
using ShopSneaker.Identity.Database.Entities;
using ShopSneaker.Identity.Model;

namespace ShopSneaker.Identity.Infrastructure.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<Users, LoginVm>();
            CreateMap<Users, UserProfileViewModel>();
        }
    }
}
