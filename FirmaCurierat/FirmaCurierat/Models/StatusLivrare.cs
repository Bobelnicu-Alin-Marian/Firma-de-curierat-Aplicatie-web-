using System.ComponentModel.DataAnnotations;

namespace FirmaCurierat.Models
{
    public class StatusLivrare
    {
        [Key]
        public int Id_status { get; set; }
        public string Denumire { get; set; }

        public int Id_colet { get; set; }
        public Colet Colet { get; set; }
    }
}