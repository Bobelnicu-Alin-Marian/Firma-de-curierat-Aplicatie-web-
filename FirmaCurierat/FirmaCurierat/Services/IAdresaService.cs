using FirmaCurierat.Models;

namespace FirmaCurierat.Services
{
    public interface IAdresaService
    {
        Task<IEnumerable<Adresa>> GetAdreseByClientIdAsync(int idClient);
        Task<IEnumerable<Adresa>> GetAdreseByUserIdAsync(string userId);
        Task<Adresa> GetAdresaByIdAsync(int id);
        Task AddAdresaAsync(Adresa adresa);
        Task UpdateAdresaAsync(Adresa adresa);
        Task DeleteAdresaAsync(int id);
    }
}