using KuaforYonetimSistemi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Swagger'� hizmetlere ekleyin
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Veritaban� ve Identity yap�land�rmas�
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity servislerini ekle
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>() // Role deste�i ekle
    .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Swagger UI ekran�n� ekle
}

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

app.UseAuthentication(); // Kimlik do�rulama middleware
app.UseAuthorization();  // Yetkilendirme middleware

// Seed Data i�in hizmetleri ba�lat
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Admin rol� ve kullan�c�s�n� olu�tur
        await SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Seed Data hatas�: {ex.Message}");
    }
}

// Varsay�lan rota
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
