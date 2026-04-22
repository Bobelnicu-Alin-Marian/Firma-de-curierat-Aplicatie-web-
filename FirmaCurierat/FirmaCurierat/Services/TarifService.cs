using FirmaCurierat.Models;
using FirmaCurierat.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirmaCurierat.Services
{
    public class TarifService : ITarifService
    {
        private readonly IRepository<Tarif> _repository;

        public TarifService(IRepository<Tarif> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Tarif>> GetAllTarifeAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Tarif> GetTarifByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddTarifAsync(Tarif tarif)
        {
            if (tarif.PretLocal <= 0 || tarif.PretNational <= 0 || tarif.PretInternational <= 0)
            {
                throw new InvalidOperationException("Prețurile trebuie să fie mai mari decât zero.");
            }

            await _repository.AddAsync(tarif);
        }

        public async Task UpdateTarifAsync(Tarif tarif)
        {
            if (tarif.PretLocal <= 0 || tarif.PretNational <= 0 || tarif.PretInternational <= 0)
            {
                throw new InvalidOperationException("Prețurile trebuie să fie mai mari decât zero.");
            }

            await _repository.UpdateAsync(tarif);
        }

        public async Task DeleteTarifAsync(int id)
        {
            var tarif = await _repository.GetByIdAsync(id);
            if (tarif != null)
            {
                await _repository.DeleteAsync(tarif);
            }
        }
    }
}