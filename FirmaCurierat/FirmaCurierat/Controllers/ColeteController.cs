using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirmaCurierat.Models;

namespace FirmaCurierat.Controllers
{
    public class ColeteController : Controller
    {
        private readonly FirmaCurieratContext _context;

        public ColeteController(FirmaCurieratContext context)
        {
            _context = context;
        }

        // GET: Colets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Colete.ToListAsync());
        }

        // GET: Colets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colet = await _context.Colete
                .FirstOrDefaultAsync(m => m.Id_colet == id);
            if (colet == null)
            {
                return NotFound();
            }

            return View(colet);
        }

        // GET: Colets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Colets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_colet,Pret,Greutate,Tip,Dimensiune")] Colet colet)
        {
            try
            {
                Random rng = new Random();

                long min = 10000000000;
                long max = 99999999999;
                long awbAleatoriu = (long)(rng.NextDouble() * (max - min) + min);

                colet.Awb = awbAleatoriu.ToString();

                colet.Id_comanda = null;

                _context.Add(colet);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Coletul a fost înregistrat cu succes! AWB: " + colet.Awb;
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Eroare la generare: " + ex.Message);
                return View(colet);
            }
        }

        // GET: Colets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colet = await _context.Colete.FindAsync(id);
            if (colet == null)
            {
                return NotFound();
            }
            return View(colet);
        }

        // POST: Colets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_colet,Awb,Pret,Greutate,Dimensiune,Tip,Id_comanda")] Colet colet)
        {
            if (id != colet.Id_colet) return NotFound();

            // ELIMINĂM "SUSPECTUL": Dacă Id_comanda e null, ModelState ar fi invalid. 
            // Spunem .NET-ului să nu-i pese de el la validare.
            ModelState.Remove("Id_comanda");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(colet);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColetExists(colet.Id_colet)) return NotFound();
                    else throw;
                }
            }

            // DACĂ TOT NU MERGE: Forțăm salvarea aici pentru a vedea dacă problema e doar validarea
            // (Șterge liniile de mai jos după ce confirmi că merge)
            _context.Update(colet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Colets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colet = await _context.Colete
                .FirstOrDefaultAsync(m => m.Id_colet == id);
            if (colet == null)
            {
                return NotFound();
            }

            return View(colet);
        }

        // POST: Colets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var colet = await _context.Colete.FindAsync(id);
            if (colet != null)
            {
                _context.Colete.Remove(colet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ColetExists(int id)
        {
            return _context.Colete.Any(e => e.Id_colet == id);
        }
    }
}
