using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FirmaCurierat.Models
{
    public class Client
    {
        [Key]
        public int Id_client { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string? Telefon { get; set; }

        // Legături (Un client poate avea mai multe adrese, comenzi și facturi)
        public ICollection<Adresa> Adrese { get; set; } = new List<Adresa>();
        public ICollection<Comanda> ComenziExpediate { get; set; } = new List<Comanda>();
        public ICollection<Comanda> ComenziPrimite { get; set; } = new List<Comanda>();
        public ICollection<Factura> Facturi { get; set; } = new List<Factura>();
    }
}