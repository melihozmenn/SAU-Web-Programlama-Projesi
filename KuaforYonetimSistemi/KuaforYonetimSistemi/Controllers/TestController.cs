using Microsoft.AspNetCore.Mvc;

namespace KuaforYonetimSistemi.Controllers
{
    public class TestController : Controller
    {
        private readonly AppDbContext _context;

        public TestController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult TestDatabaseConnection()
        {
            try
            {
                var salonlar = _context.Salons.ToList();
                return Content($"Bağlantı başarılı! Veritabanında {salonlar.Count} salon bulundu.");
            }
            catch (Exception ex)
            {
                return Content($"Veritabanı bağlantı hatası: {ex.Message}");
            }
        }
    }
}
