using FirmaCurierat.Models;
using FirmaCurierat.Repositories;

namespace FirmaCurierat.Services
{
    public class AdresaService : IAdresaService
    {
        private readonly IRepository<Adresa> _repository;

        public AdresaService(IRepository<Adresa> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Adresa>> GetAdreseByClientIdAsync(int idClient)
        {
            var toateAdresele = await _repository.GetAllAsync();
            // Filtram doar adresele care apartin acestui client
            return toateAdresele.Where(a => a.Id_client == idClient).ToList();
        }

        public async Task<Adresa> GetAdresaByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task AddAdresaAsync(Adresa adresa) => await _repository.AddAsync(adresa);

        public async Task UpdateAdresaAsync(Adresa adresa) => await _repository.UpdateAsync(adresa);

        public async Task DeleteAdresaAsync(int id)
        {
            var adresa = await _repository.GetByIdAsync(id);
            if (adresa != null) await _repository.DeleteAsync(adresa);
        }
    }
}