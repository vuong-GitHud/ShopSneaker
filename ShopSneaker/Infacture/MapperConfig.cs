﻿using AutoMapper;
using ShopSneaker.Data.Entities;
using ShopSneaker.Models;

namespace ShopSneaker.Infacture;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Product, ProductVm>();
        CreateMap<ProductVm, Product>();
        CreateMap<Product, UpdateProductVm>();
        CreateMap<UpdateProductVm, Product>();
        CreateMap<Order, OrderViewModel>();
        CreateMap<OrderViewModel, Order>();
    }
}