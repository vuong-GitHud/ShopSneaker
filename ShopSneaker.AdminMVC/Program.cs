using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ShopSneaker.AdminMVC.Infrastructure;
using ShopSneaker.Areas.Identity.Data;
using ShopSneaker.Infacture;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = "/Authen/Login";
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

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthenAPI, AuthenAPI>();
builder.Services.AddDbContext<ShopSneakerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShopSneakerContextConnection")));
builder.Services.AddAutoMapper(typeof(MapperConfig));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
