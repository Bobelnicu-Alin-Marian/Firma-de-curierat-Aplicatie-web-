using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        // Foreign Key către Comandă
        public int? Id_comanda { get; set; }
        public Comanda? Comanda { get; set; }

        // Legături către Tracking și Tranzit
        public ICollection<StatusLivrare> Statusuri { get; set; }
        public ICollection<Tranziteaza> Tranzitari { get; set; }
    }
}