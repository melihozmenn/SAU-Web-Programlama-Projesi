using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KuaforYonetimSistemi.Data; // SeedData s�n�f�n�n bulundu�u namespace
using KuaforYonetimSistemi.Models; // ApplicationUser ve di�er modellerin bulundu�u namespace

var builder = WebApplication.CreateBuilder(args);

// Swagger'� hizmetlere ekleyin
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Veritaban� ve Identity yap�land�rmas�
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    // �ifre kurallar�n� devre d��� b�rakma
    options.Password.RequireDigit = false;  // Say� gereksinimini kapat
    options.Password.RequireLowercase = false;  // K���k harf gereksinimini kapat
    options.Password.RequireNonAlphanumeric = false;  // �zel karakter gereksinimini kapat
    options.Password.RequireUppercase = false;  // B�y�k harf gereksinimini kapat
    options.Password.RequiredLength = 2;  // Minimum uzunlu�u 6'ya d���r
    options.Password.RequiredUniqueChars = 1;  // Minimum benzersiz karakter say�s�n� 1'e d���r
})
.AddRoles<IdentityRole>()
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
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Kimlik do�rulama middleware
app.UseAuthorization();  // Yetkilendirme middleware

app.MapControllerRoute(
    name: "default",  
    pattern: "{controller=Home}/{action=Index}/{id?}");


// Admin rol�n� ve admin kullan�c�y� seed et
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.Initialize(services);
}

// Uygulamay� �al��t�r
app.Run();
