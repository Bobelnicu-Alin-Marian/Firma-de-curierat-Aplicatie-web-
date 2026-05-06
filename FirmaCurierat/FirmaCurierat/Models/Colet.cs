using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirmaCurierat.Models
{
    public class Colet
    {
        [Key]
        public int Id_colet { get; set; }

        [Required]
        public string Awb { get; set; }

      
        public decimal Pret { get; set; }

        public decimal CostLivrare { get; set; }

       
        [Required]
        public string ZonaLivrare { get; set; }

        // -------------------------------

        public decimal Greutate { get; set; }
        public string? Dimensiune { get; set; }
        public string Tip { get; set; }

        public int? Id_comanda { get; set; }
        public virtual Comanda? Comanda { get; set; }

        public virtual ICollection<StatusLivrare> Statusuri { get; set; } = new List<StatusLivrare>();

        public virtual ICollection<Tranziteaza> Tranzitari { get; set; } = new List<Tranziteaza>();

        public int? Id_tarif { get; set; }
        public virtual Tarif? Tarif { get; set; }
    }
}