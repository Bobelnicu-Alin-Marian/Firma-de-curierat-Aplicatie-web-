using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirmaCurierat.Models
{
    public class Colet
    {
        [Key]
        public int Id_colet { get; set; }
        public string Awb { get; set; }
        public decimal Pret { get; set; }
        public decimal Greutate { get; set; }
        public string? Dimensiune { get; set; }
        public string Tip { get; set; }
        public int? Id_comanda { get; set; }
        public Comanda? Comanda { get; set; }

        public ICollection<StatusLivrare> Statusuri { get; set; }
        public ICollection<Tranziteaza> Tranzitari { get; set; }
        public int? Id_tarif { get; set; } 
        public Tarif? Tarif { get; set; } 
    }
}