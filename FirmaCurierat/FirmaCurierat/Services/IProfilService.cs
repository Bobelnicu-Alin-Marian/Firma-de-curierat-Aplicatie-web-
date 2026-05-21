using FirmaCurierat.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace FirmaCurierat.Services
{
    public interface IProfilService
    {
        Task<ProfilViewModel?> GetProfilAsync(string userId);
        Task<(bool Succes, IEnumerable<string> Erori)> UpdateProfilAsync(string userId, EditProfilViewModel model);
        Task<byte[]?> GetPozaProfilAsync(string userId);
        string DetectContentType(byte[] bytes);
    }
}
