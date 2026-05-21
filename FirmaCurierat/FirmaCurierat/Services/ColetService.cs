using FirmaCurierat.Models;
using FirmaCurierat.Repositories;
using Microsoft.EntityFrameworkCore;
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
        private readonly FirmaCurieratContext _context;

        public ColetService(IRepository<Colet> repository, IRepository<Tarif> tarifRepository, FirmaCurieratContext context)
        {
            _repository = repository;
            _tarifRepository = tarifRepository;
            _context = context;
        }

        public async Task<IEnumerable<Colet>> GetAllColeteAsync() { return await _repository.GetAllAsync(); }
        public async Task<Colet> GetColetByIdAsync(int id) { return await _repository.GetByIdAsync(id); }
        public async Task UpdateColetAsync(Colet colet) { await _repository.UpdateAsync(colet); }
        public async Task DeleteColetAsync(int id) { var c = await _repository.GetByIdAsync(id); if (c != null) await _repository.DeleteAsync(c); }
        public async Task<Colet> GetColetByAwbAsync(string awb) { var colete = await _repository.GetAllAsync(); return colete.FirstOrDefault(c => c.Awb == awb); }

        public async Task<Colet?> GetColetCuDetaliiAsync(int id)
        {
            return await _context.Colete
                .Include(c => c.Comanda)
                    .ThenInclude(com => com!.Expeditor)
                .Include(c => c.Comanda)
                    .ThenInclude(com => com!.Destinatar)
                .Include(c => c.Comanda)
                    .ThenInclude(com => com!.AdresaRidicare)
                .Include(c => c.Comanda)
                    .ThenInclude(com => com!.AdresaLivrare)
                .Include(c => c.Statusuri)
                .FirstOrDefaultAsync(c => c.Id_colet == id);
        }

        // Returnează toate coletele cu Comanda inclusă (pentru pagina Index/Admin)
        public async Task<IEnumerable<Colet>> GetAllColeteWithComandaAsync()
        {
            return await _context.Colete
                .Include(c => c.Comanda)
                .ToListAsync();
        }

        // Returnează doar coletele create de un anumit utilizator Identity
        public async Task<IEnumerable<Colet>> GetColeteByUserIdAsync(string userId)
        {
            return await _context.Colete
                .Include(c => c.Comanda)
                .Where(c => c.Comanda != null && c.Comanda.ApplicationUserId == userId)
                .ToListAsync();
        }

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