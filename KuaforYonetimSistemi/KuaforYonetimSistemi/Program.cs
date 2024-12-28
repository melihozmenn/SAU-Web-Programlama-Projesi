using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KuaforYonetimSistemi.Data; // SeedData sýnýfýnýn bulunduðu namespace
using KuaforYonetimSistemi.Models; // ApplicationUser ve diðer modellerin bulunduðu namespace

var builder = WebApplication.CreateBuilder(args);

// Swagger'ý hizmetlere ekleyin
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Veritabaný ve Identity yapýlandýrmasý
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    // Þifre kurallarýný devre dýþý býrakma
    options.Password.RequireDigit = false;  // Sayý gereksinimini kapat
    options.Password.RequireLowercase = false;  // Küçük harf gereksinimini kapat
    options.Password.RequireNonAlphanumeric = false;  // Özel karakter gereksinimini kapat
    options.Password.RequireUppercase = false;  // Büyük harf gereksinimini kapat
    options.Password.RequiredLength = 2;  // Minimum uzunluðu 6'ya düþür
    options.Password.RequiredUniqueChars = 1;  // Minimum benzersiz karakter sayýsýný 1'e düþür
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Swagger UI ekranýný ekle
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Kimlik doðrulama middleware
app.UseAuthorization();  // Yetkilendirme middleware

app.MapControllerRoute(
    name: "default",  
    pattern: "{controller=Home}/{action=Index}/{id?}");


// Admin rolünü ve admin kullanýcýyý seed et
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.Initialize(services);
}

// Uygulamayý çalýþtýr
app.Run();
