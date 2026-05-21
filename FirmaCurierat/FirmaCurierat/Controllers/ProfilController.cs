using FirmaCurierat.Models;
using FirmaCurierat.Models.ViewModels;
using FirmaCurierat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FirmaCurierat.Controllers
{
    [Authorize]
    public class ProfilController : Controller
    {
        private readonly IProfilService  _profilService;
        private readonly IColetService   _coletService;
        private readonly IAdresaService  _adresaService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfilController(
            IProfilService               profilService,
            IColetService                coletService,
            IAdresaService               adresaService,
            UserManager<ApplicationUser> userManager)
        {
            _profilService = profilService;
            _coletService  = coletService;
            _adresaService = adresaService;
            _userManager   = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return RedirectToAction("Index", "Home");

            var model = await _profilService.GetProfilAsync(userId);
            if (model == null) return RedirectToAction("Index", "Home");

            // Ultimele 8 colete — Admin vede toate, User vede doar ale sale
            if (User.IsInRole("Admin"))
            {
                var toate = await _coletService.GetAllColeteWithComandaAsync();
                ViewBag.Colete    = toate.OrderByDescending(c => c.Id_colet).Take(8).ToList();
                ViewBag.EsteAdmin = true;
            }
            else
            {
                var proprii = await _coletService.GetColeteByUserIdAsync(userId);
                ViewBag.Colete    = proprii.OrderByDescending(c => c.Id_colet).Take(8).ToList();
                ViewBag.EsteAdmin = false;

                // Adresele salvate ale utilizatorului curent
                var adrese = await _adresaService.GetAdreseByUserIdAsync(userId);
                ViewBag.Adrese = adrese.ToList();
            }

            return View(model);
        }

        public async Task<IActionResult> GetPoza()
        {
            var userId = _userManager.GetUserId(User);
            if (userId != null)
            {
                var bytes = await _profilService.GetPozaProfilAsync(userId);
                if (bytes != null)
                    return File(bytes, _profilService.DetectContentType(bytes));
            }

            var svg = """
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 100 100">
                  <circle cx="50" cy="50" r="50" fill="#ffcc00"/>
                  <circle cx="50" cy="38" r="20" fill="#555"/>
                  <ellipse cx="50" cy="95" rx="32" ry="28" fill="#555"/>
                </svg>
                """;
            return Content(svg, "image/svg+xml");
        }

        [HttpGet]
        public async Task<IActionResult> Editeaza()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return RedirectToAction("Index", "Home");

            var profil = await _profilService.GetProfilAsync(userId);
            if (profil == null) return RedirectToAction("Index", "Home");

            var model = new EditProfilViewModel
            {
                Nume        = profil.Nume        ?? string.Empty,
                Prenume     = profil.Prenume     ?? string.Empty,
                Email       = profil.Email       ?? string.Empty,
                PhoneNumber = profil.PhoneNumber,
                ArePoza     = profil.ArePoza
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editeaza(EditProfilViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var userId = _userManager.GetUserId(User);
            if (userId == null) return RedirectToAction("Index", "Home");

            var (succes, erori) = await _profilService.UpdateProfilAsync(userId, model);
            if (succes)
            {
                TempData["Succes"] = "Profilul a fost actualizat cu succes.";
                return RedirectToAction("Index");
            }

            foreach (var eroare in erori)
                ModelState.AddModelError(string.Empty, eroare);

            return View(model);
        }
    }
}
