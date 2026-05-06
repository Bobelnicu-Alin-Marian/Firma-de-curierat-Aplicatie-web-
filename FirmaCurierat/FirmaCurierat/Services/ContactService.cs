using FirmaCurierat.Models;
using FirmaCurierat.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirmaCurierat.Services
{
    public class ContactService : IContactService
    {
        private readonly IRepository<Contact> _repository;

        public ContactService(IRepository<Contact> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Contact>> GetAllContacteAsync() => await _repository.GetAllAsync();
        public async Task<Contact> GetContactByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task AddContactAsync(Contact contact) => await _repository.AddAsync(contact);
        public async Task UpdateContactAsync(Contact contact) => await _repository.UpdateAsync(contact);

        public async Task DeleteContactAsync(int id)
        {
            var contact = await _repository.GetByIdAsync(id);
            if (contact != null) await _repository.DeleteAsync(contact);
        }
    }
}