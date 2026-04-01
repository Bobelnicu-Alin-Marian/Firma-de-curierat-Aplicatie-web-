using System.ComponentModel.DataAnnotations;

namespace FirmaCurierat.Models
{
    public class Tranziteaza
    {
        [Key]
        public int Id_tranziteaza { get; set; }

        public int Id_hub { get; set; }
        public Hub Hub { get; set; }

        public int Id_colet { get; set; }
        public Colet Colet { get; set; }
    }
}