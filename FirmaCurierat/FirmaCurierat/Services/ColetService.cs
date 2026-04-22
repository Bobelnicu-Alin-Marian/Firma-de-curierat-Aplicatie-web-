using FirmaCurierat.Models;
using FirmaCurierat.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirmaCurierat.Services
{
    public class ColetService : IColetService
    {
        // legatura cu repositoryul
        private readonly IRepository<Colet> _repository;

        public ColetService(IRepository<Colet> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Colet>> GetAllColeteAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Colet> GetColetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddColetAsync(Colet colet)
        {
            // logica de generare awb
            Random rng = new Random();
            long min = 10000000000;
            long max = 99999999999;
            long awbAleatoriu = (long)(rng.NextDouble() * (max - min) + min);

            colet.Awb = awbAleatoriu.ToString();
            colet.Id_comanda = null;

            // trimitere colet catre repository
            await _repository.AddAsync(colet);
        }

        public async Task UpdateColetAsync(Colet colet)
        {
            await _repository.UpdateAsync(colet);
        }

        public async Task DeleteColetAsync(int id)
        {
            var colet = await _repository.GetByIdAsync(id);
            if (colet != null)
            {
                await _repository.DeleteAsync(colet);
            }
        }
    }
}