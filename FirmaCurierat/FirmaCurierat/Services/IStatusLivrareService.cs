using FirmaCurierat.Models;

namespace FirmaCurierat.Services
{
    public interface IStatusLivrareService
    {
        Task<IEnumerable<StatusLivrare>> GetIstoricByColetIdAsync(int idColet);
        Task AdaugaStatusNouAsync(int idColet, string denumire, string locatie);
    }
}