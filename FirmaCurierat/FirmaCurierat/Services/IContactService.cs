using FirmaCurierat.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirmaCurierat.Services
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllContacteAsync();
        Task<Contact> GetContactByIdAsync(int id);
        Task AddContactAsync(Contact contact);
        Task UpdateContactAsync(Contact contact);
        Task DeleteContactAsync(int id);
    }
}