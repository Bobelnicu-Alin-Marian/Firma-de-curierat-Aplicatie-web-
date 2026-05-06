using Microsoft.AspNetCore.Mvc;
using FirmaCurierat.Models;
using FirmaCurierat.Services;

namespace FirmaCurierat.Controllers
{
    public class AdreseController : Controller
    {
        private readonly IAdresaService _adresaService;

        // Simulăm că utilizatorul cu ID-ul 1 este logat în aplicație
        private readonly int _idClientLogat = 1;

        public AdreseController(IAdresaService adresaService)
        {
            _adresaService = adresaService;
        }

        // 1. LISTA ADRESELOR (Tabelul din Profil)
        public async Task<IActionResult> Index()
        {
            var adrese = await _adresaService.GetAdreseByClientIdAsync(_idClientLogat);
            return View(adrese);
        }

        // 2. FORMULAR ADĂUGARE (Afișare)
        public IActionResult Create()
        {
            return View();
        }

        // 3. SALVARE ADRESĂ NOUĂ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tara,Judet,Localitate,Strada,Numar")] Adresa adresa)
        {
            // Ignorăm proprietățile de navigare pentru validare
            ModelState.Remove("Client");
            ModelState.Remove("Hub");

            if (ModelState.IsValid)
            {
                adresa.Id_client = _idClientLogat;
                await _adresaService.AddAdresaAsync(adresa);

                TempData["MesajSucces"] = "✅ Adresa a fost salvată cu succes!";
                return RedirectToAction(nameof(Create));
            }

            return View(adresa);
        }

        // 4. FORMULAR EDITARE (Afișare)
        public async Task<IActionResult> Edit(int id)
        {
            var adresa = await _adresaService.GetAdresaByIdAsync(id);
            if (adresa == null || adresa.Id_client != _idClientLogat) return NotFound();

            return View(adresa);
        }

        // 5. SALVARE EDITARE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_adresa,Tara,Judet,Localitate,Strada,Numar")] Adresa adresa)
        {
            if (id != adresa.Id_adresa) return NotFound();

            if (ModelState.IsValid)
            {
                adresa.Id_client = _idClientLogat; // Îl păstrăm pe același client
                await _adresaService.UpdateAdresaAsync(adresa);
                return RedirectToAction(nameof(Index));
            }
            return View(adresa);
        }

        // GET: Adrese/Delete/5 (Afișează pagina cu detaliile adresei)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var adresa = await _adresaService.GetAdresaByIdAsync(id.Value);

            // Verificăm dacă adresa există și dacă aparține clientului logat
            if (adresa == null || adresa.Id_client != _idClientLogat) return NotFound();

            return View(adresa);
        }

        // POST: Adrese/Delete/5 (Execută ștergerea reală)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adresa = await _adresaService.GetAdresaByIdAsync(id);

            if (adresa != null && adresa.Id_client == _idClientLogat)
            {
                await _adresaService.DeleteAdresaAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}