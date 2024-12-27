using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KuaforYonetimSistemi.Models;

namespace KuaforYonetimSistemi.Controllers
{
    public class RandevuController : Controller
    {
        private readonly AppDbContext _context;

        public RandevuController(AppDbContext context)
        {
            _context = context;
        }
        // Randevu Onay Sayfasý
        public async Task<IActionResult> Onay(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Randevu bilgilerini getirme
            var randevu = await _context.Randevus
                .Include(r => r.Calisan)
                .Include(r => r.Islem)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }
        // GET: Randevu
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Randevus.Include(r => r.Calisan).Include(r => r.Islem);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Randevu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevus
                .Include(r => r.Calisan)
                .Include(r => r.Islem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }
        // GET: Randevu/Create
        public IActionResult Create()
        {
            // Çalýþanlarý ve iþlemleri ViewData'ya gönderiyoruz
            ViewData["CalisanId"] = new SelectList(_context.Calisans, "Id", "Adi");
            ViewData["IslemId"] = new SelectList(_context.Islems, "Id", "Salon");
            return View();
        }
        // POST: Randevu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tarih,IslemId,CalisanId,MusteriAdi,MusteriTelefonu")] Randevu randevu)
        {
            // Çakýþma kontrolü: Ayný çalýþan ve tarih için bir randevu olup olmadýðýný kontrol ediyoruz
            var mevcutRandevu = await _context.Randevus
                .Where(r => r.CalisanId == randevu.CalisanId && r.Tarih == randevu.Tarih)
                .FirstOrDefaultAsync();

            if (mevcutRandevu != null)
            {
                // Eðer çakýþma varsa, model hatasý ekliyoruz
                ModelState.AddModelError("", "Bu tarih ve saatte zaten bir randevu var.");
                ViewData["CalisanId"] = new SelectList(_context.Calisans, "Id", "Adi", randevu.CalisanId);
                ViewData["IslemId"] = new SelectList(_context.Islems, "Id", "Salon", randevu.IslemId);
                return View(randevu);  // Eðer çakýþma varsa, tekrar formu göster
            }

            if (ModelState.IsValid)
            {
                _context.Add(randevu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));  // Randevu baþarýlý bir þekilde kaydedildi
            }

            // Model hatasý varsa, gerekli verileri tekrar görünüme gönderiyoruz
            ViewData["CalisanId"] = new SelectList(_context.Calisans, "Id", "Adi", randevu.CalisanId);
            ViewData["IslemId"] = new SelectList(_context.Islems, "Id", "Salon", randevu.IslemId);
            return View(randevu);
        }

        // GET: Randevu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevus.FindAsync(id);
            if (randevu == null)
            {
                return NotFound();
            }
            ViewData["CalisanId"] = new SelectList(_context.Calisans, "Id", "Adi", randevu.CalisanId);
            ViewData["IslemId"] = new SelectList(_context.Islems, "Id", "Salon", randevu.IslemId);
            return View(randevu);
        }

        // POST: Randevu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tarih,IslemId,CalisanId,MusteriAdi,MusteriTelefonu")] Randevu randevu)
        {
            if (id != randevu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(randevu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RandevuExists(randevu.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CalisanId"] = new SelectList(_context.Calisans, "Id", "Adi", randevu.CalisanId);
            ViewData["IslemId"] = new SelectList(_context.Islems, "Id", "Salon", randevu.IslemId);
            return View(randevu);
        }
        // Randevu Onayla (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Onay(int id)
        {
            var randevu = await _context.Randevus.FindAsync(id);
            if (randevu == null)
            {
                return NotFound();
            }

            // Randevuyu onayla (örneðin, "Onaylandý" durumunu ekliyoruz)
            randevu.Onayli = true;

            try
            {
                _context.Update(randevu);  // Randevuyu güncelle
                await _context.SaveChangesAsync();  // Deðiþiklikleri kaydet
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RandevuExists(randevu.Id))  // Randevu hala var mý kontrol et
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));  // Onay iþlemi tamamlandýktan sonra listeye yönlendir
        }
        // GET: Randevu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevus
                .Include(r => r.Calisan)
                .Include(r => r.Islem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }
        
        // POST: Randevu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var randevu = await _context.Randevus.FindAsync(id);
            if (randevu != null)
            {
                _context.Randevus.Remove(randevu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RandevuExists(int id)
        {
            return _context.Randevus.Any(e => e.Id == id);
        }
    }
}
