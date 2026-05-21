using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FirmaCurierat.Models.ViewModels
{
    public class EditProfilViewModel
    {
        [Required(ErrorMessage = "Numele este obligatoriu.")]
        [Display(Name = "Nume")]
        public string Nume { get; set; } = string.Empty;

        [Required(ErrorMessage = "Prenumele este obligatoriu.")]
        [Display(Name = "Prenume")]
        public string Prenume { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email-ul este obligatoriu.")]
        [EmailAddress(ErrorMessage = "Adresă de email invalidă.")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Număr de telefon invalid.")]
        [Display(Name = "Telefon")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Fotografie de profil")]
        public IFormFile? PozaProfilFile { get; set; }

        public bool ArePoza { get; set; }
    }
}
