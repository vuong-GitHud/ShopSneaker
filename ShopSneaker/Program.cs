using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopSneaker.Areas.Identity.Data;
using ShopSneaker.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ShopSneaker.Infacture;
using ShopSneaker.Infacture.Emplement;
using ShopSneaker.Infacture.interfaces;
using ShopSneaker.Infacture.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ShopSneakerContextConnection") ?? throw new InvalidOperationException("Connection string 'ShopSneakerContextConnection' not found.");

builder.Services.AddDbContext<ShopSneakerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShopSneakerContextConnection")));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/AuthenCustomer/Login";
        options.Cookie.HttpOnly = false;
        options.Cookie.IsEssential = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.None;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromDays(60);
        //options.DataProtectionProvider = _provider;
        //options.TicketDataFormat = new TicketDataFormat(_provider.CreateProtector(SecurityConstant.AQSecurityMasterProtector));
        //options.Events.OnValidatePrincipal = PrincipalSecurityStampValidator.ValidatePrincipalAsync;
    });
builder.Services.ConfigureApplicationCookie(options =>
{

    //options.Cookie.Domain = GetCookieDomain();
    options.Cookie.HttpOnly = false;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.None;
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(60);
});

builder.Services.AddAutoMapper(typeof(MapperConfig));

// Add services to the container.
builder.Services.AddControllersWithViews();
// builder.Services.AddTransient<IAuthenApi, AuthenApi>();
builder.Services.AddHttpClient<IAuthenApi, AuthenApi>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();

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

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
