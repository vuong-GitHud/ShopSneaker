using AutoMapper;
using ShopSneaker.Data.Entities;
using ShopSneaker.Models;

namespace ShopSneaker.Infacture;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Product, ProductVm>();
    }
}