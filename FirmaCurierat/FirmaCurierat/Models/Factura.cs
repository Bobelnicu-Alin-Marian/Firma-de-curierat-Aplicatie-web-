using System;
using System.ComponentModel.DataAnnotations;

namespace FirmaCurierat.Models
{
    public class Factura
    {
        [Key]
        public int Id_factura { get; set; }
        public DateTime Data { get; set; }
        public decimal Valoare { get; set; }

        public int Id_client { get; set; }
        public Client Client { get; set; }

        public int Id_comanda { get; set; }
        public Comanda Comanda { get; set; }
    }
}