using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirmaCurierat.Models;
using FirmaCurierat.Services; // namespace-ul pentru Servicii

namespace FirmaCurierat.Controllers
{
    public class ColeteController : Controller
    {
        private readonly IColetService _coletService;

        // introducem serviciul prin constructor
        public ColeteController(IColetService coletService)
        {
            _coletService = coletService;
        }

        // GET: Colete
        public async Task<IActionResult> Index()
        {
            var colete = await _coletService.GetAllColeteAsync();
            return View(colete);
        }

        // GET: Colete/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var colet = await _coletService.GetColetByIdAsync(id.Value);
            if (colet == null) return NotFound();

            return View(colet);
        }

        // GET: Colete/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Colete/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_colet,Pret,Greutate,Tip,Dimensiune")] Colet colet)
        {
            try
            {
                
                await _coletService.AddColetAsync(colet);

                
                TempData["SuccessMessage"] = "Coletul a fost adaugat cu succes! AWB: " + colet.Awb;
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Eroare la înregistrare: " + ex.Message);
                return View(colet);
            }
        }

        // GET: Colete/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var colet = await _coletService.GetColetByIdAsync(id.Value);
            if (colet == null) return NotFound();

            return View(colet);
        }

        // POST: Colete/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_colet,Awb,Pret,Greutate,Dimensiune,Tip,Id_comanda")] Colet colet)
        {
            if (id != colet.Id_colet) return NotFound();

            ModelState.Remove("Id_comanda");

            if (ModelState.IsValid)
            {
                try
                {
                    // Folosim serviciul pentru a face update
                    await _coletService.UpdateColetAsync(colet);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    if (!await ColetExists(colet.Id_colet)) return NotFound();
                    else throw;
                }
            }

            return View(colet);
        }

        // GET: Colete/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var colet = await _coletService.GetColetByIdAsync(id.Value);
            if (colet == null) return NotFound();

            return View(colet);
        }

        // POST: Colete/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Folosim serviciul pentru stergere
            await _coletService.DeleteColetAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ColetExists(int id)
        {
            var colet = await _coletService.GetColetByIdAsync(id);
            return colet != null;
        }
    }
}