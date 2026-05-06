using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirmaCurierat.Models
{
    public class StatusLivrare
    {
        [Key]
        public int Id_status { get; set; }

        [Required]
        public string Denumire { get; set; }

        // Adăugăm data și locația pentru timeline
        public DateTime Data_actualizare { get; set; } = DateTime.Now;
        public string? Locatie { get; set; }

        
        public int Id_colet { get; set; }

        [ForeignKey("Id_colet")] // Îi spunem clar lui EF să folosească coloana de mai sus!
        public virtual Colet? Colet { get; set; }
    }
}