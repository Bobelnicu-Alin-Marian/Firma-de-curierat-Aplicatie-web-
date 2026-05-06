using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirmaCurierat.Models
{
    public class Adresa
    {
        [Key]
        public int Id_adresa { get; set; }

        public string Tara { get; set; }
        public string Judet { get; set; }
        public string Localitate { get; set; }
        public string Strada { get; set; }
        public string Numar { get; set; }

        // Client
        public int? Id_client { get; set; }

        [ForeignKey("Id_client")]
        public Client Client { get; set; }

     
       // public int? Id_hub { get; set; }

        //[ForeignKey("Id_hub")] 
       // public Hub Hub { get; set; }
    }
}