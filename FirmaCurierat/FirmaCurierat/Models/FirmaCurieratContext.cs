using Microsoft.EntityFrameworkCore;

namespace FirmaCurierat.Models
{
    // Clasa de context derivă din DbContext și reprezintă o sesiune de lucru cu baza de date
    public class FirmaCurieratContext : DbContext
    {
        // Constructorul care primește opțiunile de configurare (ex: string-ul de conexiune)
        public FirmaCurieratContext(DbContextOptions<FirmaCurieratContext> options) : base(options)
        {
        }

        // Seturile de entități (Entity Sets) care vor fi transformate în tabele
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

        // Metoda folosită pentru configurări avansate ale modelului
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. Configurarea preciziei pentru zecimale
            modelBuilder.Entity<Colet>().Property(c => c.Pret).HasPrecision(18, 2);
            modelBuilder.Entity<Colet>().Property(c => c.Greutate).HasPrecision(18, 2);
            modelBuilder.Entity<Factura>().Property(f => f.Valoare).HasPrecision(18, 2);

            // 2. REZOLVARE EROARE: Restricții pentru tabelele de legătură (Tranzitari, Conduceri)
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

            // 3. Reguli pentru Comenzi (cele pe care le aveai deja)
            modelBuilder.Entity<Comanda>()
                .HasOne(c => c.AdresaRidicare).WithMany().HasForeignKey(c => c.Id_adresa_ridicare).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comanda>()
                .HasOne(c => c.AdresaLivrare).WithMany().HasForeignKey(c => c.Id_adresa_livrare).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comanda>()
                .HasOne(c => c.Expeditor).WithMany(cl => cl.ComenziExpediate).HasForeignKey(c => c.Id_expeditor).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comanda>()
                .HasOne(c => c.Destinatar).WithMany(cl => cl.ComenziPrimite).HasForeignKey(c => c.Id_destinatar).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comanda>()
                .HasOne(c => c.Operator).WithMany().HasForeignKey(c => c.Id_operator).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comanda>()
                .HasOne(c => c.Curier).WithMany().HasForeignKey(c => c.Id_curier).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Comanda).WithMany(c => c.Facturi).HasForeignKey(f => f.Id_comanda).OnDelete(DeleteBehavior.Restrict);

            // Restricție suplimentară pentru Hub și Adresă
            modelBuilder.Entity<Hub>()
                .HasOne(h => h.Adresa)
                .WithMany()
                .HasForeignKey(h => h.Id_adresa)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}