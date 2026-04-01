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

        // Legături (Un client poate avea mai multe adrese, comenzi și facturi)
        public ICollection<Adresa> Adrese { get; set; }
        public ICollection<Comanda> ComenziExpediate { get; set; }
        public ICollection<Comanda> ComenziPrimite { get; set; }
        public ICollection<Factura> Facturi { get; set; }
    }
}