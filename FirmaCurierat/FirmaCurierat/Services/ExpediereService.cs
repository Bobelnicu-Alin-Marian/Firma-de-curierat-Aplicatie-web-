using FirmaCurierat.Models;
using FirmaCurierat.Models.ViewModels;
using FirmaCurierat.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FirmaCurierat.Services
{
    public class ExpediereService : IExpediereService
    {
        private readonly FirmaCurieratContext _context;
        private readonly IRepository<Tarif> _tarifRepository;

        public ExpediereService(FirmaCurieratContext context, IRepository<Tarif> tarifRepository)
        {
            _context = context;
            _tarifRepository = tarifRepository;
        }

        public async Task<Colet> CreazaExpeditieAsync(CreareColetViewModel model, ApplicationUser expeditor)
        {
            // 1. Client Expeditor — creat din datele profilului utilizatorului logat
            var clientExpeditor = new Client
            {
                Nume     = expeditor.Nume     ?? expeditor.Email ?? "Necunoscut",
                Prenume  = expeditor.Prenume  ?? string.Empty,
                Telefon  = expeditor.PhoneNumber
            };

            // 2. Client Destinatar — introdus manual în formular
            var clientDestinatar = new Client
            {
                Nume    = model.DestinatarNume,
                Prenume = model.DestinatarPrenume,
                Telefon = model.DestinatarTelefon
            };

            // 3. Adresa de ridicare (expeditor)
            var adresaRidicare = new Adresa
            {
                Tara       = model.RidicareTara,
                Judet      = model.RidcareJudet,
                Localitate = model.RidcareLocalitate,
                Strada     = model.RidcareStrada,
                Numar      = model.RidcareNumar
            };

            // 4. Adresa de livrare (destinatar)
            var adresaLivrare = new Adresa
            {
                Tara       = model.LivrareTara,
                Judet      = model.LivrareJudet,
                Localitate = model.LivrareLocalitate,
                Strada     = model.LivrareStrada,
                Numar      = model.LivrareNumar
            };

            // 5. Comanda — leagă totul
            var comanda = new Comanda
            {
                Status            = "Înregistrată",
                Expeditor         = clientExpeditor,
                Destinatar        = clientDestinatar,
                AdresaRidicare    = adresaRidicare,
                AdresaLivrare     = adresaLivrare,
                ApplicationUserId = expeditor.Id   // legătura cu userul Identity logat
            };

            // 6. Colet — cu AWB unic și cost calculat din tabela Tarife
            var colet = new Colet
            {
                Awb         = GenereazaAwb(),
                Tip         = model.Tip,
                Dimensiune  = model.Dimensiune,
                ZonaLivrare = model.ZonaLivrare,
                Greutate    = model.Greutate,
                Pret        = model.Pret,
                CostLivrare = await CalculeazaCostAsync(model.Tip, model.ZonaLivrare),
                Comanda     = comanda
            };

            // EF Core rezolvă automat toate FK-urile și salvează totul într-o singură tranzacție
            _context.Colete.Add(colet);
            await _context.SaveChangesAsync();

            return colet;
        }

        // ── Metode private ───────────────────────────────────────────────

        private static string GenereazaAwb()
        {
            var rng = new Random();
            long min = 10_000_000_000L;
            long max = 99_999_999_999L;
            return ((long)(rng.NextDouble() * (max - min) + min)).ToString();
        }

        private async Task<decimal> CalculeazaCostAsync(string tip, string zona)
        {
            if (string.IsNullOrEmpty(tip)) return 0;

            var tarife = await _tarifRepository.GetAllAsync();
            var tarif  = tarife.FirstOrDefault(t =>
                t.CategorieGreutate != null &&
                t.CategorieGreutate.ToLower() == tip.ToLower());

            if (tarif == null) return 0;

            return zona?.ToLower() switch
            {
                "local"         => tarif.PretLocal,
                "national"      => tarif.PretNational,
                "international" => tarif.PretInternational,
                _               => 0
            };
        }
    }
}
