using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirmaCurierat.Models;
using FirmaCurierat.Repositories;

namespace FirmaCurierat.Services
{
    public class StatusLivrareService : IStatusLivrareService
    {
        private readonly IRepository<StatusLivrare> _statusRepository;

        public StatusLivrareService(IRepository<StatusLivrare> statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<IEnumerable<StatusLivrare>> GetIstoricByColetIdAsync(int idColet)
        {
            var toate = await _statusRepository.GetAllAsync();
            return toate.Where(s => s.Id_colet == idColet).OrderBy(s => s.Data_actualizare);
        }

        public async Task AdaugaStatusNouAsync(int idColet, string denumire, string locatie)
        {
            var status = new StatusLivrare
            {
                Id_colet = idColet,
                Denumire = denumire,
                Locatie = locatie,
                Data_actualizare = DateTime.Now
            };
            await _statusRepository.AddAsync(status);
        }
    }
}