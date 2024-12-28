using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KuaforYonetimSistemi.Models;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class RandevuApiController : ControllerBase
{
    private readonly AppDbContext _context;

    public RandevuApiController(AppDbContext context)
    {
        _context = context;
    }

    // Tüm Randevuları Getir
    [HttpGet]
    public IActionResult GetAll()
    {
        var randevular = _context.Randevus
            .Include(r => r.Calisan)
            .Include(r => r.Islem)
            .Select(r => new
            {
                r.Id,
                r.MusteriAdi,
                r.MusteriTelefonu,
                r.Tarih,
                Calisan = r.Calisan.Adi,
                Islem = r.Islem.Adi,
                r.Onayli
            }).ToList();

        return Ok(randevular);
    }

    // Belirli Çalışan ile Randevuları Filtrele
    [HttpGet("calisan/{calisanId}")]
    public IActionResult GetByCalisan(int calisanId)
    {
        var randevular = _context.Randevus
            .Where(r => r.CalisanId == calisanId)
            .Include(r => r.Calisan)
            .Include(r => r.Islem)
            .Select(r => new
            {
                r.Id,
                r.MusteriAdi,
                r.MusteriTelefonu,
                r.Tarih,
                Calisan = r.Calisan.Adi,
                Islem = r.Islem.Adi,
                r.Onayli
            }).ToList();

        if (!randevular.Any())
            return NotFound($"Çalışan ID {calisanId} ile eşleşen randevu bulunamadı.");

        return Ok(randevular);
    }

    // Tarihe Göre Randevuları Filtrele
    [HttpGet("tarih")]
    public IActionResult GetByDate(DateTime tarih)
    {
        var randevular = _context.Randevus
            .Where(r => r.Tarih.Date == tarih.Date)
            .Include(r => r.Calisan)
            .Include(r => r.Islem)
            .Select(r => new
            {
                r.Id,
                r.MusteriAdi,
                r.MusteriTelefonu,
                r.Tarih,
                Calisan = r.Calisan.Adi,
                Islem = r.Islem.Adi,
                r.Onayli
            }).ToList();

        if (!randevular.Any())
            return NotFound($"Tarih {tarih:dd/MM/yyyy} için randevu bulunamadı.");

        return Ok(randevular);
    }

    // Onaylı Randevuları Listele
    [HttpGet("onayli")]
    public IActionResult GetApproved()
    {
        var randevular = _context.Randevus
            .Where(r => r.Onayli)
            .Include(r => r.Calisan)
            .Include(r => r.Islem)
            .Select(r => new
            {
                r.Id,
                r.MusteriAdi,
                r.MusteriTelefonu,
                r.Tarih,
                Calisan = r.Calisan.Adi,
                Islem = r.Islem.Adi,
                r.Onayli
            }).ToList();

        return Ok(randevular);
    }
}
