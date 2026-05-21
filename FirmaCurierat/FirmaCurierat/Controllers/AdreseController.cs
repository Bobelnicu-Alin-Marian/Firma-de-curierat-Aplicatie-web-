using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using FirmaCurierat.Models;
using FirmaCurierat.Services;

namespace FirmaCurierat.Controllers
{
    [Authorize]
    public class AdreseController : Controller
    {
        private readonly IAdresaService _adresaService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdreseController(IAdresaService adresaService, UserManager<ApplicationUser> userManager)
        {
            _adresaService = adresaService;
            _userManager   = userManager;
        }

        // GET: Adrese/Index — redirecționează la profil (lista e acolo)
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Profil");
        }

        // GET: Adrese/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Adrese/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tara,Judet,Localitate,Strada,Numar")] Adresa adresa)
        {
            ModelState.Remove("Client");
            ModelState.Remove("ApplicationUserId");

            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                adresa.ApplicationUserId = userId;
                await _adresaService.AddAdresaAsync(adresa);

                TempData["Succes"] = "Adresa a fost salvată cu succes!";
                return RedirectToAction("Index", "Profil");
            }

            return View(adresa);
        }

        // GET: Adrese/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var adresa = await _adresaService.GetAdresaByIdAsync(id);
            var userId = _userManager.GetUserId(User);

            if (adresa == null || adresa.ApplicationUserId != userId) return NotFound();

            return View(adresa);
        }

        // POST: Adrese/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_adresa,Tara,Judet,Localitate,Strada,Numar")] Adresa adresa)
        {
            if (id != adresa.Id_adresa) return NotFound();

            ModelState.Remove("Client");
            ModelState.Remove("ApplicationUserId");

            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                // Verificare că adresa aparține userului curent
                var adresaExistenta = await _adresaService.GetAdresaByIdAsync(id);
                if (adresaExistenta == null || adresaExistenta.ApplicationUserId != userId)
                    return NotFound();

                adresa.ApplicationUserId = userId;
                await _adresaService.UpdateAdresaAsync(adresa);

                TempData["Succes"] = "Adresa a fost actualizată cu succes!";
                return RedirectToAction("Index", "Profil");
            }

            return View(adresa);
        }

        // GET: Adrese/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var adresa = await _adresaService.GetAdresaByIdAsync(id.Value);
            var userId = _userManager.GetUserId(User);

            if (adresa == null || adresa.ApplicationUserId != userId) return NotFound();

            return View(adresa);
        }

        // POST: Adrese/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adresa = await _adresaService.GetAdresaByIdAsync(id);
            var userId = _userManager.GetUserId(User);

            if (adresa != null && adresa.ApplicationUserId == userId)
            {
                await _adresaService.DeleteAdresaAsync(id);
                TempData["Succes"] = "Adresa a fost ștearsă.";
            }

            return RedirectToAction("Index", "Profil");
        }
    }
}
