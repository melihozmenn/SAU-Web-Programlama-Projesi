using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KuaforYonetimSistemi.Models;

namespace KuaforYonetimSistemi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalonApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SalonApiController(AppDbContext context)
        {
            _context = context;
        }

        // Salonları listeleyen API endpoint'i
        [HttpGet]
        public async Task<IActionResult> GetSalons()
        {
            var salons = await _context.Salons
                .Select(s => new
                {
                    s.Id,
                    s.Adi,
                    Calisan = s.Calisans.Select(i => new { i.Adi, i.UzmanlikAlanlari })
                })
                .ToListAsync();

            return Ok(salons);  // JSON formatında dönecek
        }

        // Belirli bir salona ait çalışanları listeleyen API endpoint'i
        [HttpGet("{salonId}/calisans")]
        public async Task<IActionResult> GetCalisansBySalon(int salonId)
        {
            var calisans = await _context.Calisans
                .Where(c => c.SalonId == salonId)
                .Select(c => new
                {
                    c.Id,
                    c.Adi,
                    c.UzmanlikAlanlari
                })
                .ToListAsync();

            if (!calisans.Any())
            {
                return NotFound(new { Message = "Belirtilen salona ait çalışan bulunamadı." });
            }

            return Ok(calisans);
        }
    }
}
