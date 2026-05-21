using Microsoft.AspNetCore.Identity;

namespace FirmaCurierat.Models
{
    // mostenim IdentityUser
    public class ApplicationUser : IdentityUser
    {
        public byte[]? PozaProfil { get; set; }
        public string? Nume { get; set; }
        public string? Prenume { get; set; }
    }
}