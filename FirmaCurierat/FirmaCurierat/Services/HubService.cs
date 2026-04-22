using FirmaCurierat.Models;
using FirmaCurierat.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirmaCurierat.Services
{
    public class HubService : IHubService
    {
        // injectam repository-ul pentru a avea acces la date
        private readonly IRepository<Hub> _repository;

        public HubService(IRepository<Hub> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Hub>> GetAllHubsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Hub> GetHubByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddHubAsync(Hub hub)
        {
            await _repository.AddAsync(hub);
        }

        public async Task UpdateHubAsync(Hub hub)
        {
            await _repository.UpdateAsync(hub);
        }

        public async Task DeleteHubAsync(int id)
        {
            var hub = await _repository.GetByIdAsync(id);
            if (hub != null)
            {
                await _repository.DeleteAsync(hub);
            }
        }
    }
}