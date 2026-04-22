using FirmaCurierat.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirmaCurierat.Services
{
    public interface ITarifService
    {
        Task<IEnumerable<Tarif>> GetAllTarifeAsync();
        Task<Tarif> GetTarifByIdAsync(int id);
        Task AddTarifAsync(Tarif tarif);
        Task UpdateTarifAsync(Tarif tarif);
        Task DeleteTarifAsync(int id);
    }
}