using System.ComponentModel.DataAnnotations;

namespace FirmaCurierat.Models
{
    public class Adresa
    {
        [Key]
        public int Id_adresa { get; set; }
        public string Tara { get; set; }
        public string Judet { get; set; }
        public string Localitate { get; set; }
        public string Numar { get; set; }

        // Foreign Key către Client (am pus 'int?' pentru a permite adrese fără client obligatoriu, ex: adresa Hub-ului)
        public int? Id_client { get; set; }
        public Client Client { get; set; }
    }
}