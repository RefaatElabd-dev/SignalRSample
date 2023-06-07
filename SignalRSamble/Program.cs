using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SignalRSamble.Data;
using SignalRSamble.Hubs;
using StackExchange.Redis;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddCors(p => p.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.AddControllersWithViews();

builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = builder.Configuration["RedisCacheUrl"];});

//builder.Services.AddSignalR();
builder.Services.AddSignalR().AddStackExchangeRedis(builder.Configuration["ClusteredRedisServers"], options =>
{
    options.Configuration.ChannelPrefix = "MyApp";
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("MyPolicy");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapHub<UserHub>("/hubs/userCount");
app.MapHub<DeathlyHallowsHub>("/hubs/deathlyHallows");
app.MapHub<HouseGroupHub>("/hubs/houseGroup");

app.Run();
