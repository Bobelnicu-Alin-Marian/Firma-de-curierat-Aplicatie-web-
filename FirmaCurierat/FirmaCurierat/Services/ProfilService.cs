using FirmaCurierat.Models;
using FirmaCurierat.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace FirmaCurierat.Services
{
    public class ProfilService : IProfilService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfilService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ProfilViewModel?> GetProfilAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            return new ProfilViewModel
            {
                Nume = user.Nume,
                Prenume = user.Prenume,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ArePoza = user.PozaProfil != null && user.PozaProfil.Length > 0
            };
        }

        public async Task<(bool Succes, IEnumerable<string> Erori)> UpdateProfilAsync(string userId, EditProfilViewModel model)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return (false, ["Utilizatorul nu a fost găsit."]);

            user.Nume = model.Nume;
            user.Prenume = model.Prenume;
            user.PhoneNumber = model.PhoneNumber;

            if (user.Email != model.Email)
            {
                user.Email = model.Email;
                user.UserName = model.Email;
            }

            if (model.PozaProfilFile != null && model.PozaProfilFile.Length > 0)
            {
                using var ms = new MemoryStream();
                await model.PozaProfilFile.CopyToAsync(ms);
                user.PozaProfil = ms.ToArray();
            }

            var result = await _userManager.UpdateAsync(user);
            return (result.Succeeded, result.Errors.Select(e => e.Description));
        }

        public async Task<byte[]?> GetPozaProfilAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user?.PozaProfil != null && user.PozaProfil.Length > 0)
                return user.PozaProfil;

            return null;
        }

        public string DetectContentType(byte[] bytes)
        {
            if (bytes.Length >= 2 && bytes[0] == 0xFF && bytes[1] == 0xD8) return "image/jpeg";
            if (bytes.Length >= 4 && bytes[0] == 0x89 && bytes[1] == 0x50) return "image/png";
            if (bytes.Length >= 3 && bytes[0] == 0x47 && bytes[1] == 0x49) return "image/gif";
            return "image/jpeg";
        }
    }
}
