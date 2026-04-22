using FirmaCurierat.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirmaCurierat.Services
{
    public interface IHubService
    {
        Task<IEnumerable<Hub>> GetAllHubsAsync();
        Task<Hub> GetHubByIdAsync(int id);
        Task AddHubAsync(Hub hub);
        Task UpdateHubAsync(Hub hub);
        Task DeleteHubAsync(int id);
    }
}