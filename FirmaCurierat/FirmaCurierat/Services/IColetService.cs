using FirmaCurierat.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirmaCurierat.Services
{
    public interface IColetService
    {
        Task<IEnumerable<Colet>> GetAllColeteAsync();
        Task<Colet> GetColetByIdAsync(int id);
        Task AddColetAsync(Colet colet);
        Task UpdateColetAsync(Colet colet);
        Task DeleteColetAsync(int id);
        Task<Colet> GetColetByAwbAsync(string awb);
        Task<Colet?> GetColetCuDetaliiAsync(int id);
        Task<IEnumerable<Colet>> GetColeteByUserIdAsync(string userId);
        Task<IEnumerable<Colet>> GetAllColeteWithComandaAsync();
    }
}