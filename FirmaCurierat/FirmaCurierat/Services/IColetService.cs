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
    }
}