using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FirmaCurierat.Models
{
    public class Vehicul
    {
        [Key]
        public int Id_vehicul { get; set; }
        public string Marca { get; set; }

        public ICollection<Conduce> Conduceri { get; set; }
    }
}