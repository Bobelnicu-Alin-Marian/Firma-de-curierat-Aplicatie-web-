using Microsoft.AspNetCore.Mvc;
using FirmaCurierat.Models;
using FirmaCurierat.Services;
using System.Threading.Tasks;

namespace FirmaCurierat.Controllers
{
    public class TarifeController : Controller
    {
        private readonly ITarifService _tarifService;

        public TarifeController(ITarifService tarifService)
        {
            _tarifService = tarifService;
        }

        public async Task<IActionResult> Index()
        {
            var tarife = await _tarifService.GetAllTarifeAsync();
            return View(tarife);
        }

        // GET: Tarife/Create (Afiseaza formularul gol pentru adaaugare)
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tarife/Create (Primeste datele din formular și le salveaza)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_tarif,CategorieGreutate,PretLocal,PretNational,PretInternational")] Tarif tarif)
        {
            if (ModelState.IsValid)
            {
                await _tarifService.AddTarifAsync(tarif);
                TempData["SuccessMessage"] = "Tariful a fost adăugat cu succes!";
                return RedirectToAction(nameof(Index));
            }
            return View(tarif);
        }

        // GET: Tarife/Edit/5 (Afisează formularul completat cu datele existente)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var tarif = await _tarifService.GetTarifByIdAsync(id.Value);
            if (tarif == null) return NotFound();

            return View(tarif);
        }

        // POST: Tarife/Edit/5 (Salveaza modificarile)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_tarif,CategorieGreutate,PretLocal,PretNational,PretInternational")] Tarif tarif)
        {
            if (id != tarif.Id_tarif) return NotFound();

            if (ModelState.IsValid)
            {
                await _tarifService.UpdateTarifAsync(tarif);
                TempData["SuccessMessage"] = "Tariful a fost actualizat cu succes!";
                return RedirectToAction(nameof(Index));
            }
            return View(tarif);
        }

        // GET: Tarife/Delete/5 (Afiseaza pagina de confirmare a stergerii)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var tarif = await _tarifService.GetTarifByIdAsync(id.Value);
            if (tarif == null) return NotFound();

            return View(tarif);
        }

        // POST: Tarife/Delete/5 (Executa stergerea propriu-zisa)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _tarifService.DeleteTarifAsync(id);
            TempData["SuccessMessage"] = "Tariful a fost șters!";
            return RedirectToAction(nameof(Index));
        }
        // GET: Tarife/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var tarif = await _tarifService.GetTarifByIdAsync(id.Value);
            if (tarif == null) return NotFound();

            return View(tarif);
        }
    }
}