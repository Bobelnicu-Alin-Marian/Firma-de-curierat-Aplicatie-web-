using FirmaCurierat.Models;
using FirmaCurierat.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaCurierat.Services
{
    public class ColetService : IColetService
    {
        private readonly IRepository<Colet> _repository;
        private readonly IRepository<Tarif> _tarifRepository;

        public ColetService(IRepository<Colet> repository, IRepository<Tarif> tarifRepository)
        {
            _repository = repository;
            _tarifRepository = tarifRepository;
        }

        public async Task<IEnumerable<Colet>> GetAllColeteAsync() { return await _repository.GetAllAsync(); }
        public async Task<Colet> GetColetByIdAsync(int id) { return await _repository.GetByIdAsync(id); }
        public async Task UpdateColetAsync(Colet colet) { await _repository.UpdateAsync(colet); }
        public async Task DeleteColetAsync(int id) { var c = await _repository.GetByIdAsync(id); if (c != null) await _repository.DeleteAsync(c); }
        public async Task<Colet> GetColetByAwbAsync(string awb) { var colete = await _repository.GetAllAsync(); return colete.FirstOrDefault(c => c.Awb == awb); }

        public async Task AddColetAsync(Colet colet)
        {
            Random rng = new Random();
            long min = 10000000000;
            long max = 99999999999;
            long awbAleatoriu = (long)(rng.NextDouble() * (max - min) + min);

            colet.Awb = awbAleatoriu.ToString();
            colet.Id_comanda = null;

            
            colet.CostLivrare = await CalculeazaCostDinBazaDeDate(colet.Tip, colet.ZonaLivrare);

            await _repository.AddAsync(colet);
        }

        private async Task<decimal> CalculeazaCostDinBazaDeDate(string tipTrimitere, string zona)
        {
            if (string.IsNullOrEmpty(tipTrimitere)) return 0;

            var toateTarifele = await _tarifRepository.GetAllAsync();

            
            var tarifGasit = toateTarifele.FirstOrDefault(t =>
                t.CategorieGreutate != null &&
                t.CategorieGreutate.ToLower() == tipTrimitere.ToLower());

            if (tarifGasit == null)
                return 0; 

            string zonaLower = zona?.ToLower() ?? "";

            if (zonaLower == "local") return tarifGasit.PretLocal;
            if (zonaLower == "national") return tarifGasit.PretNational;
            if (zonaLower == "international") return tarifGasit.PretInternational;

            return 0;
        }
    }
}