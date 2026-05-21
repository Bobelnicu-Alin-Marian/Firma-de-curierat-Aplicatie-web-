using System.ComponentModel.DataAnnotations;

namespace FirmaCurierat.Models.ViewModels
{
    public class CreareColetViewModel
    {
        // ── Expeditor (pre-completat din profil, read-only) ─────────────
        public string ExpeditorNume { get; set; } = string.Empty;
        public string ExpeditorPrenume { get; set; } = string.Empty;
        public string? ExpeditorTelefon { get; set; }

        // ── Adresa ridicare ──────────────────────────────────────────────
        [Required(ErrorMessage = "Țara este obligatorie.")]
        [Display(Name = "Țară")]
        public string RidicareTara { get; set; } = "România";

        [Required(ErrorMessage = "Județul este obligatoriu.")]
        [Display(Name = "Județ")]
        public string RidcareJudet { get; set; } = string.Empty;

        [Required(ErrorMessage = "Localitatea este obligatorie.")]
        [Display(Name = "Localitate")]
        public string RidcareLocalitate { get; set; } = string.Empty;

        [Required(ErrorMessage = "Strada este obligatorie.")]
        [Display(Name = "Stradă")]
        public string RidcareStrada { get; set; } = string.Empty;

        [Required(ErrorMessage = "Numărul este obligatoriu.")]
        [Display(Name = "Număr")]
        public string RidcareNumar { get; set; } = string.Empty;

        // ── Destinatar ───────────────────────────────────────────────────
        [Required(ErrorMessage = "Numele destinatarului este obligatoriu.")]
        [Display(Name = "Nume")]
        public string DestinatarNume { get; set; } = string.Empty;

        [Required(ErrorMessage = "Prenumele destinatarului este obligatoriu.")]
        [Display(Name = "Prenume")]
        public string DestinatarPrenume { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefonul destinatarului este obligatoriu.")]
        [Phone(ErrorMessage = "Număr de telefon invalid.")]
        [Display(Name = "Telefon")]
        public string DestinatarTelefon { get; set; } = string.Empty;

        // ── Adresa livrare ───────────────────────────────────────────────
        [Required(ErrorMessage = "Țara este obligatorie.")]
        [Display(Name = "Țară")]
        public string LivrareTara { get; set; } = "România";

        [Required(ErrorMessage = "Județul este obligatoriu.")]
        [Display(Name = "Județ")]
        public string LivrareJudet { get; set; } = string.Empty;

        [Required(ErrorMessage = "Localitatea este obligatorie.")]
        [Display(Name = "Localitate")]
        public string LivrareLocalitate { get; set; } = string.Empty;

        [Required(ErrorMessage = "Strada este obligatorie.")]
        [Display(Name = "Stradă")]
        public string LivrareStrada { get; set; } = string.Empty;

        [Required(ErrorMessage = "Numărul este obligatoriu.")]
        [Display(Name = "Număr")]
        public string LivrareNumar { get; set; } = string.Empty;

        // ── Detalii colet ────────────────────────────────────────────────
        [Required(ErrorMessage = "Tipul coletului este obligatoriu.")]
        [Display(Name = "Tip trimitere")]
        public string Tip { get; set; } = "Standard";

        [Display(Name = "Dimensiune")]
        public string? Dimensiune { get; set; }

        [Required(ErrorMessage = "Zona de livrare este obligatorie.")]
        [Display(Name = "Zonă livrare")]
        public string ZonaLivrare { get; set; } = string.Empty;

        [Required(ErrorMessage = "Greutatea este obligatorie.")]
        [Range(0.01, 9999, ErrorMessage = "Greutatea trebuie să fie mai mare ca 0.")]
        [Display(Name = "Greutate (kg)")]
        public decimal Greutate { get; set; }

        [Display(Name = "Valoare declarată (RON)")]
        public decimal Pret { get; set; }
    }
}
