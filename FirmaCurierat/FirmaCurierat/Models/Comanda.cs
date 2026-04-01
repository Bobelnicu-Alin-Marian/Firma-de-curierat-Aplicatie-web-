using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace FirmaCurierat.Models
{
    public class Comanda
    {
        [Key]
        public int Id_comanda { get; set; }
        public string Status { get; set; }

        // Legături Client (Expeditor și Destinatar)
        public int Id_expeditor { get; set; }
        public Client Expeditor { get; set; }

        public int Id_destinatar { get; set; }
        public Client Destinatar { get; set; }

        // Legături Adrese
        public int Id_adresa_ridicare { get; set; }
        public Adresa AdresaRidicare { get; set; }

        public int Id_adresa_livrare { get; set; }
        public Adresa AdresaLivrare { get; set; }

        // Legături Angajați (am folosit 'int?' pentru a putea fi Nule până când comanda e preluată)
        public int? Id_operator { get; set; }
        public Operator Operator { get; set; }

        public int? Id_curier { get; set; }
        public Curier Curier { get; set; }

        // Legătură Hub
        public int? Id_hub { get; set; }
        public Hub Hub { get; set; }

        // Colecții
        public ICollection<Colet> Colete { get; set; }
        public ICollection<Factura> Facturi { get; set; }
    }
}