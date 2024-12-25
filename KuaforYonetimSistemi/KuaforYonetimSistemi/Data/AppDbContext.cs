using KuaforYonetimSistemi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Salon> Salons { get; set; }
    public DbSet<Calisan> Calisans { get; set; }
    public DbSet<Islem> Islems { get; set; }
    public DbSet<Randevu> Randevus { get; set; }
    public DbSet<Kullanici> Kullanicis { get; set; }
}

