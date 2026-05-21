using FirmaCurierat.Models;
using FirmaCurierat.Models.ViewModels;

namespace FirmaCurierat.Services
{
    public interface IExpediereService
    {
        Task<Colet> CreazaExpeditieAsync(CreareColetViewModel model, ApplicationUser expeditor);
    }
}
