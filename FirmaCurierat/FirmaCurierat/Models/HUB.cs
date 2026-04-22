using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FirmaCurierat.Models
{
    public class Hub
    {
        [Key]
        public int Id_hub { get; set; }
        public int Capacitate { get; set; }
        public string Oras { get; set; }

        // Foreign Key către Adresa Hub-ului
        public int? Id_adresa { get; set; }
        public Adresa? Adresa { get; set; }

        public ICollection<Tranziteaza>? Tranzitari { get; set; }
        public ICollection<Comanda>? Comenzi { get; set; }
    }
}