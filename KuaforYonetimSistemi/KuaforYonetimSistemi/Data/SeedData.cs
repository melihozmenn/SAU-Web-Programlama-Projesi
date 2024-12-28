using KuaforYonetimSistemi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KuaforYonetimSistemi.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                // Admin rolünü oluştur
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                // Admin kullanıcısını oluştur ve admin rolünü ata
                var adminEmail = "b201210083@sakarya.edu.tr";
                if (await userManager.FindByEmailAsync(adminEmail) == null)
                {
                    var user = new ApplicationUser { UserName = adminEmail, Email = adminEmail };
                    var result = await userManager.CreateAsync(user, "sau");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                }
            }
        }
    }

}



