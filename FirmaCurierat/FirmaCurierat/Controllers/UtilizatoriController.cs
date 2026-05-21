using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FirmaCurierat.Models;

namespace FirmaCurierat.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UtilizatoriController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole>    _roleManager;

        public UtilizatoriController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole>    roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // ── Index ─────────────────────────────────────────────────────────────
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var model = new List<UtilizatorViewModel>();
            foreach (var u in users)
            {
                var roles = await _userManager.GetRolesAsync(u);
                model.Add(new UtilizatorViewModel
                {
                    Id      = u.Id,
                    Email   = u.Email ?? "—",
                    Nume    = u.Nume,
                    Prenume = u.Prenume,
                    Telefon = u.PhoneNumber,
                    Rol     = roles.FirstOrDefault() ?? "Fără rol"
                });
            }
            return View(model);
        }

        // ── Create GET ────────────────────────────────────────────────────────
        public IActionResult Create() => View(new UtilizatorCreateViewModel());

        // ── Create POST ───────────────────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UtilizatorCreateViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = new ApplicationUser
            {
                UserName       = vm.Email,
                Email          = vm.Email,
                EmailConfirmed = true,
                Nume           = vm.Nume,
                Prenume        = vm.Prenume,
                PhoneNumber    = vm.Telefon
            };

            var result = await _userManager.CreateAsync(user, vm.Parola);
            if (!result.Succeeded)
            {
                foreach (var e in result.Errors)
                    ModelState.AddModelError(string.Empty, e.Description);
                return View(vm);
            }

            await _userManager.AddToRoleAsync(user, vm.Rol ?? "User");
            TempData["SuccessMessage"] = $"Contul pentru {user.Email} a fost creat cu succes.";
            return RedirectToAction(nameof(Index));
        }

        // ── Edit GET ──────────────────────────────────────────────────────────
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);
            return View(new UtilizatorEditViewModel
            {
                Id      = user.Id,
                Email   = user.Email ?? string.Empty,
                Nume    = user.Nume,
                Prenume = user.Prenume,
                Telefon = user.PhoneNumber,
                Rol     = roles.FirstOrDefault() ?? "User"
            });
        }

        // ── Edit POST ─────────────────────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UtilizatorEditViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = await _userManager.FindByIdAsync(vm.Id);
            if (user == null) return NotFound();

            // Date de bază
            user.Nume    = vm.Nume;
            user.Prenume = vm.Prenume;

            if (user.Email != vm.Email)
            {
                await _userManager.SetEmailAsync(user, vm.Email);
                await _userManager.SetUserNameAsync(user, vm.Email);
            }

            if (user.PhoneNumber != vm.Telefon)
                await _userManager.SetPhoneNumberAsync(user, vm.Telefon);

            await _userManager.UpdateAsync(user);

            // Rol — nu se poate modifica propriul cont
            var currentUserId = _userManager.GetUserId(User);
            if (vm.Id != currentUserId)
            {
                var rolesCurente = await _userManager.GetRolesAsync(user);
                if (!rolesCurente.Contains(vm.Rol))
                {
                    await _userManager.RemoveFromRolesAsync(user, rolesCurente);
                    await _userManager.AddToRoleAsync(user, vm.Rol ?? "User");
                }
            }

            // Parolă nouă (opțional)
            if (!string.IsNullOrWhiteSpace(vm.ParolaNoua))
            {
                var token      = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passResult = await _userManager.ResetPasswordAsync(user, token, vm.ParolaNoua);
                if (!passResult.Succeeded)
                {
                    foreach (var e in passResult.Errors)
                        ModelState.AddModelError(string.Empty, e.Description);
                    return View(vm);
                }
            }

            TempData["SuccessMessage"] = $"Contul {user.Email} a fost actualizat cu succes.";
            return RedirectToAction(nameof(Index));
        }

        // ── SetRol ────────────────────────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetRol(string userId, string rolNou)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(rolNou))
                return BadRequest();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var currentUserId = _userManager.GetUserId(User);
            if (userId == currentUserId)
            {
                TempData["Eroare"] = "Nu îți poți schimba propriul rol.";
                return RedirectToAction(nameof(Index));
            }

            var rolesCurente = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, rolesCurente);
            await _userManager.AddToRoleAsync(user, rolNou);

            TempData["SuccessMessage"] = $"Rolul utilizatorului {user.Email} a fost setat la '{rolNou}'.";
            return RedirectToAction(nameof(Index));
        }

        // ── Delete ────────────────────────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return BadRequest();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var currentUserId = _userManager.GetUserId(User);
            if (userId == currentUserId)
            {
                TempData["Eroare"] = "Nu îți poți șterge propriul cont.";
                return RedirectToAction(nameof(Index));
            }

            await _userManager.DeleteAsync(user);
            TempData["SuccessMessage"] = $"Utilizatorul {user.Email} a fost șters.";
            return RedirectToAction(nameof(Index));
        }
    }

    // ── ViewModels ────────────────────────────────────────────────────────────

    public class UtilizatorViewModel
    {
        public string  Id      { get; set; } = string.Empty;
        public string  Email   { get; set; } = string.Empty;
        public string? Nume    { get; set; }
        public string? Prenume { get; set; }
        public string? Telefon { get; set; }
        public string  Rol     { get; set; } = string.Empty;
    }

    public class UtilizatorCreateViewModel
    {
        [Required(ErrorMessage = "Email-ul este obligatoriu.")]
        [EmailAddress(ErrorMessage = "Format email invalid.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Parola este obligatorie.")]
        [MinLength(6, ErrorMessage = "Parola trebuie să aibă cel puțin 6 caractere.")]
        public string Parola { get; set; } = string.Empty;

        public string? Nume    { get; set; }
        public string? Prenume { get; set; }
        public string? Telefon { get; set; }

        [Required]
        public string Rol { get; set; } = "User";
    }

    public class UtilizatorEditViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email-ul este obligatoriu.")]
        [EmailAddress(ErrorMessage = "Format email invalid.")]
        public string Email { get; set; } = string.Empty;

        public string? Nume    { get; set; }
        public string? Prenume { get; set; }
        public string? Telefon { get; set; }

        [Required]
        public string Rol { get; set; } = "User";

        [MinLength(6, ErrorMessage = "Parola trebuie să aibă cel puțin 6 caractere.")]
        public string? ParolaNoua { get; set; }
    }
}
