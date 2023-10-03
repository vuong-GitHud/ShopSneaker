using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopSneaker.Areas.Identity.Data;
using ShopSneaker.Data.Entities;
using ShopSneaker.Infacture;
using System.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using ShopSneaker.Admin.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ShopSneakerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShopSneakerContextConnection")));
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<ShopSneakerContext>()
    .AddDefaultTokenProviders();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserManagerService, UserManagerService>();

builder.Services.AddAuthentication("Authentication")
    .AddCookie("Authentication", options =>
    {
        options.AccessDeniedPath = new PathString("/access");
        options.LoginPath = new PathString("/login");
        options.ReturnUrlParameter = "RequestPath";
        options.SlidingExpiration = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
// app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
