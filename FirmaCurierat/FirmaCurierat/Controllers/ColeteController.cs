using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using FirmaCurierat.Models;
using FirmaCurierat.Models.ViewModels;
using FirmaCurierat.Services;

namespace FirmaCurierat.Controllers
{
    [Authorize]   // orice acțiune cere autentificare, dacă nu e specificat altfel
    public class ColeteController : Controller
    {
        private readonly IColetService _coletService;
        private readonly IStatusLivrareService _statusService;
        private readonly IExpediereService _expediereService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ColeteController(
            IColetService coletService,
            IStatusLivrareService statusService,
            IExpediereService expediereService,
            UserManager<ApplicationUser> userManager)
        {
            _coletService     = coletService;
            _statusService    = statusService;
            _expediereService = expediereService;
            _userManager      = userManager;
        }

        // ── Index ────────────────────────────────────────────────────────
        // Admin → toate coletele | User → doar coletele sale
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                ViewBag.Titlu    = "Toate Coletele";
                ViewBag.EsteAdmin = true;
                var toate = await _coletService.GetAllColeteWithComandaAsync();
                return View(toate);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Index", "Home");

            ViewBag.Titlu    = "Coletele Mele";
            ViewBag.EsteAdmin = false;
            var proprii = await _coletService.GetColeteByUserIdAsync(user.Id);
            return View(proprii);
        }

        // ── AWB Tracking (public) ────────────────────────────────────────
        [AllowAnonymous]
        public async Task<IActionResult> Awb(string awbNumber)
        {
            ViewBag.AwbNumber = awbNumber;
            if (string.IsNullOrEmpty(awbNumber)) return View();

            var colet = await _coletService.GetColetByAwbAsync(awbNumber);
            if (colet != null)
            {
                var istoric = await _statusService.GetIstoricByColetIdAsync(colet.Id_colet);
                colet.Statusuri = istoric.ToList();
            }
            return View(colet);
        }

        // ── UpdateStatus (Admin only) ────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(int id_colet, string awb, string denumire, string locatie)
        {
            if (id_colet <= 0 || string.IsNullOrEmpty(denumire))
                return BadRequest("Date invalide.");

            await _statusService.AdaugaStatusNouAsync(id_colet, denumire, locatie);
            return RedirectToAction("Awb", new { awbNumber = awb });
        }

        // ── Details ──────────────────────────────────────────────────────
        // Admin vede orice; User vede doar coletele sale
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var colet = await _coletService.GetColetCuDetaliiAsync(id.Value);
            if (colet == null) return NotFound();

            // Userul normal poate vedea doar coletele sale
            if (!User.IsInRole("Admin"))
            {
                var user = await _userManager.GetUserAsync(User);
                if (colet.Comanda?.ApplicationUserId != user?.Id)
                    return Forbid();
            }

            return View(colet);
        }

        // ── Create ───────────────────────────────────────────────────────
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);

            var model = new CreareColetViewModel
            {
                ExpeditorNume    = user?.Nume     ?? string.Empty,
                ExpeditorPrenume = user?.Prenume  ?? string.Empty,
                ExpeditorTelefon = user?.PhoneNumber
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreareColetViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Index", "Home");

            try
            {
                var colet = await _expediereService.CreazaExpeditieAsync(model, user);
                TempData["SuccessMessage"] = $"Expediere înregistrată! AWB: {colet.Awb} | Cost livrare: {colet.CostLivrare:F2} RON";
                return RedirectToAction(nameof(Details), new { id = colet.Id_colet });
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.InnerException?.Message
                         ?? ex.InnerException?.Message
                         ?? ex.Message;
                ModelState.AddModelError("", $"Eroare la înregistrare: {inner}");
                return View(model);
            }
        }

        // ── Edit (Admin only) ─────────────────────────────────────────────
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var colet = await _coletService.GetColetByIdAsync(id.Value);
            if (colet == null) return NotFound();
            return View(colet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id_colet,Awb,Pret,CostLivrare,ZonaLivrare,Greutate,Dimensiune,Tip,Id_comanda")] Colet colet)
        {
            if (id != colet.Id_colet) return NotFound();
            ModelState.Remove("Id_comanda");
            ModelState.Remove("Statusuri");
            ModelState.Remove("Tranzitari");

            if (ModelState.IsValid)
            {
                try { await _coletService.UpdateColetAsync(colet); return RedirectToAction(nameof(Index)); }
                catch { if (!await ColetExists(colet.Id_colet)) return NotFound(); else throw; }
            }
            return View(colet);
        }

        // ── Delete (Admin only) ───────────────────────────────────────────
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var colet = await _coletService.GetColetByIdAsync(id.Value);
            if (colet == null) return NotFound();
            return View(colet);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _coletService.DeleteColetAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ColetExists(int id) =>
            await _coletService.GetColetByIdAsync(id) != null;
    }
}
