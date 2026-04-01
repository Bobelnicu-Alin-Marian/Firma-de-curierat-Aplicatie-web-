using System.ComponentModel.DataAnnotations;

namespace FirmaCurierat.Models
{
    public class Conduce
    {
        [Key]
        public int Id_conduce { get; set; }

        public int Id_curier { get; set; }
        public Curier Curier { get; set; }

        public int Id_vehicul { get; set; }
        public Vehicul Vehicul { get; set; }
    }
}