using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ShopSneaker.Identity.Database;
using ShopSneaker.Identity.Database.Entities;
using ShopSneaker.Identity.Infrastructure.Helper;
using ShopSneaker.Identity.Infrastructure.Implement;
using ShopSneaker.Identity.Infrastructure.Interface;
using ShopSneaker.Identity.Infrastructure.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string str = builder.Configuration.GetConnectionString("IdentityDbContext");
builder.Services.AddDbContext<IdentityDbContext>();
builder.Services.AddIdentity<Users, Roles>()
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddDefaultTokenProviders();
const string ALLOWED_USERNAME_CHARACTERS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/";
builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.TryAddScoped<IUserService, UserService>();
builder.Services.Configure<JwtTokenOption>(builder.Configuration.GetSection(JwtTokenOption.JwtTokenOptionImportConfig));
builder.Services.AddStaticHelper();
builder.Services.AddOptions();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public static class ServiceCollectionExtension
{

    public static void AddStaticHelper(this IServiceCollection services)
    {
        DependencyInjectionHelper.Init(ref services);
    }
}