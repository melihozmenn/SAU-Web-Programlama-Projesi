using KuaforYonetimSistemi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Salon> Salons { get; set; }
    public DbSet<Calisan> Calisans { get; set; }
    public DbSet<Islem> Islems { get; set; }
    public DbSet<Randevu> Randevus { get; set; }
    public DbSet<Kullanici> Kullanicis { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Islem tablosuna başlangıç verisi ekleyin
        modelBuilder.Entity<Islem>().HasData(
            new Islem
            {
                Id = 1, // Eğer Id otomatik artıyorsa, burayı manuel belirtmek gerekebilir
                Adi = "Saç Kesimi",
                Sure = 30,
                Ucret = 100.00m
            },
            new Islem
            {
                Id = 2,
                Adi = "Saç Boyama",
                Sure = 45,
                Ucret = 150.00m
            },
            new Islem
            {
                Id = 3,
                Adi = "Saç Şekillendirme",
                Sure = 60,
                Ucret = 200.00m
            },
            new Islem
            {
                Id = 4,
                Adi = "Manikür ve Pedikür",
                Sure = 60,
                Ucret = 300.00m
            },

            new Islem
            {
                Id = 5,
                Adi = "Tıraş",
                Sure = 60,
                Ucret = 100.00m
            },
            new Islem
            {
                Id = 6,
                Adi = "Saç Bakımı",
                Sure = 60,
                Ucret = 500.00m
            }
        );
    }
}
