using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FirmaCurierat.Models
{
    public class FirmaCurieratContext : IdentityDbContext<ApplicationUser>
    {
        public FirmaCurieratContext(DbContextOptions<FirmaCurieratContext> options) : base(options)
        {
        }

        // Entity Sets care vor fi transformate in tabele
        public DbSet<Client> Clienti { get; set; }
        public DbSet<Adresa> Adrese { get; set; }
        public DbSet<Angajat> Angajati { get; set; }
        public DbSet<Curier> Curieri { get; set; }
        public DbSet<Operator> Operatori { get; set; }
        public DbSet<Hub> Huburi { get; set; }
        public DbSet<Comanda> Comenzi { get; set; }
        public DbSet<Colet> Colete { get; set; }
        public DbSet<Factura> Facturi { get; set; }
        public DbSet<Vehicul> Vehicule { get; set; }
        public DbSet<Conduce> Conduceri { get; set; }
        public DbSet<StatusLivrare> StatusuriLivrare { get; set; }
        public DbSet<Tranziteaza> Tranzitari { get; set; }
        public DbSet<Tarif> Tarife { get; set; }

        // NOU: Tabelul pentru datele de contact editabile
        public DbSet<Contact> Contacte { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Colet>().Property(c => c.Pret).HasPrecision(18, 2);
            modelBuilder.Entity<Colet>().Property(c => c.Greutate).HasPrecision(18, 2);
            modelBuilder.Entity<Colet>().Property(c => c.CostLivrare).HasPrecision(18, 2);
            modelBuilder.Entity<Factura>().Property(f => f.Valoare).HasPrecision(18, 2);
            modelBuilder.Entity<Tarif>().Property(t => t.PretLocal).HasPrecision(18, 2);
            modelBuilder.Entity<Tarif>().Property(t => t.PretNational).HasPrecision(18, 2);
            modelBuilder.Entity<Tarif>().Property(t => t.PretInternational).HasPrecision(18, 2);


            modelBuilder.Entity<Tranziteaza>()
                .HasOne(t => t.Colet)
                .WithMany(c => c.Tranzitari)
                .HasForeignKey(t => t.Id_colet)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tranziteaza>()
                .HasOne(t => t.Hub)
                .WithMany(h => h.Tranzitari)
                .HasForeignKey(t => t.Id_hub)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Conduce>()
                .HasOne(c => c.Curier)
                .WithMany()
                .HasForeignKey(c => c.Id_curier)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comanda>()
                .HasOne(c => c.AdresaRidicare).WithMany().HasForeignKey(c => c.Id_adresa_ridicare).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comanda>()
                .HasOne(c => c.AdresaLivrare).WithMany().HasForeignKey(c => c.Id_adresa_livrare).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comanda>()
                .HasOne(c => c.Expeditor).WithMany(cl => cl.ComenziExpediate).HasForeignKey(c => c.Id_expeditor).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comanda>()
                .HasOne(c => c.Destinatar).WithMany(cl => cl.ComenziPrimite).HasForeignKey(c => c.Id_destinatar).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comanda>()
                .HasOne(c => c.Operator).WithMany().HasForeignKey(c => c.Id_operator).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comanda>()
                .HasOne(c => c.Curier).WithMany().HasForeignKey(c => c.Id_curier).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            // Hub.Comenzi leagă explicit cele două navigări într-o singură relație
            // eliminând coloana shadow HubId_hub generată prin convenție
            modelBuilder.Entity<Comanda>()
                .HasOne(c => c.Hub).WithMany(h => h.Comenzi).HasForeignKey(c => c.Id_hub).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Comanda).WithMany(c => c.Facturi).HasForeignKey(f => f.Id_comanda).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StatusLivrare>()
                .HasOne(s => s.Colet)
                .WithMany(c => c.Statusuri)
                .HasForeignKey(s => s.Id_colet)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}