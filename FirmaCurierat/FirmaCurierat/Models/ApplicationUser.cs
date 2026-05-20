using Microsoft.AspNetCore.Identity;

namespace FirmaCurierat.Models
{
    // mostenim IdentityUser
    public class ApplicationUser : IdentityUser
    {
        // poza profil
        public byte[]? PozaProfil { get; set; }
    }
}