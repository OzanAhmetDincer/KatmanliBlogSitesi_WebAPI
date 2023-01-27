using KatmanliBlogSitesi.Data;
using KatmanliBlogSitesi.Data.Abstract;
using KatmanliBlogSitesi.Data.Concrete;
using KatmanliBlogSitesi.Service.Abstract;
using KatmanliBlogSitesi.Service.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>(); // veritaban� tabolar�m�z� temsil eden entity framework class�."DatabaseContext" projemize tan�tt�k. Bu �zel bir class bunu tan�tmam�z laz�m


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.LoginPath = "/Admin/Login"; // admin controller da authorize ekledi�imizde giri� yapmayan kullan�c�lar� admin/login sayfas�na y�nlendirir
    x.Cookie.Name = "AdminLogin";
});// admin login i�lemi ekledi�imiz zaman bu kod login i�lemi i�in program� haz�rl�yor, servisleri aya�a kald�r�yor.

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient(typeof(IService<>), typeof(Service<>));

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

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
