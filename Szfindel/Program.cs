using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Szfindel.Interface;
using Szfindel.Models;
using Szfindel.Repo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUser, UserRepo>();
builder.Services.AddScoped<IHobby, HobbyRepo>();
builder.Services.AddScoped<IAccount, AccountRepo>();

builder.Services.AddScoped<IMessage, MessageRepo>();
builder.Services.AddScoped<IMatch, MatchRepo>();


builder.Services.AddScoped<IApi, ApiRepo>();


// Add services to the container.  
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Projekt1")));
 
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.LoginPath = "/User/Login";
        options.LogoutPath = "/User/Logout";
    });


builder.Services.AddSession();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".Szfindel.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Ustaw czas trwania sesji
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

app.UseSession();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthorization();
app.UseAuthorization();
 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

 app.Run();
//////Witam