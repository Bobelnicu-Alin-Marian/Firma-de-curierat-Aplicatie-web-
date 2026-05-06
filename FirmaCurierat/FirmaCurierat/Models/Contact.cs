using System.ComponentModel.DataAnnotations;

namespace FirmaCurierat.Models
{
    public class Contact
    {
        [Key]
        public int Id_contact { get; set; }

        [Required(ErrorMessage = "Titlul este obligatoriu.")]
        [Display(Name = "Tip contact (ex: Call Center)")]
        public string Metoda { get; set; }

        [Required(ErrorMessage = "Valoarea este obligatorie.")]
        [Display(Name = "Informație (ex: 021 555 9999)")]
        public string Valoare { get; set; }

        [Display(Name = "Detalii extra (ex: L-V: 08-20)")]
        public string? Detalii { get; set; }

    }
}