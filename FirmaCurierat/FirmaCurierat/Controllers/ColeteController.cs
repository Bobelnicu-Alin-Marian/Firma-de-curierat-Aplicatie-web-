using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirmaCurierat.Models;
using FirmaCurierat.Services;

namespace FirmaCurierat.Controllers
{
    public class ColeteController : Controller
    {
        private readonly IColetService _coletService;
        private readonly IStatusLivrareService _statusService; 

       
        public ColeteController(IColetService coletService, IStatusLivrareService statusService)
        {
            _coletService = coletService;
            _statusService = statusService;
        }

       
        public async Task<IActionResult> Index()
        {
            var colete = await _coletService.GetAllColeteAsync();
            return View(colete);
        }

      
        public async Task<IActionResult> Awb(string awbNumber)
        {
            ViewBag.AwbNumber = awbNumber;

            if (string.IsNullOrEmpty(awbNumber))
            {
                return View();
            }

          
            var colet = await _coletService.GetColetByAwbAsync(awbNumber);

            if (colet != null)
            {
                
                var istoric = await _statusService.GetIstoricByColetIdAsync(colet.Id_colet);
                colet.Statusuri = istoric.ToList();
            }

            return View(colet);
        }

        // POST: Colete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id_colet, string awb, string denumire, string locatie)
        {
            if (id_colet <= 0 || string.IsNullOrEmpty(denumire))
            {
                return BadRequest("Date invalide.");
            }

            
            await _statusService.AdaugaStatusNouAsync(id_colet, denumire, locatie);

            return RedirectToAction("Awb", new { awbNumber = awb });
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var colet = await _coletService.GetColetByIdAsync(id.Value);
            if (colet == null) return NotFound();

            return View(colet);
        }


        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
 
        public async Task<IActionResult> Create([Bind("Greutate,Pret,Tip,Dimensiune,ZonaLivrare")] Colet colet)
        {
            try
            {
                
                await _coletService.AddColetAsync(colet);

              
                TempData["SuccessMessage"] = $"Coletul a fost adăugat cu succes! AWB: {colet.Awb} | Cost Livrare: {colet.CostLivrare} RON";

                
                return RedirectToAction(nameof(Create));
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
       
        public async Task<IActionResult> Edit(int id, [Bind("Id_colet,Awb,Pret,CostLivrare,ZonaLivrare,Greutate,Dimensiune,Tip,Id_comanda")] Colet colet)
        {
            if (id != colet.Id_colet) return NotFound();

            ModelState.Remove("Id_comanda");
            ModelState.Remove("Statusuri");
            ModelState.Remove("Tranzitari"); 

            if (ModelState.IsValid)
            {
                try
                {
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