using Aspnet_mvc.Data;
using Aspnet_mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace Aspnet_mvc.Repository;

public class ContactRepository : IContactRepository
{
    private readonly ContactContext _ContactContext;

    public ContactRepository(ContactContext contactContext)
    {
        _ContactContext = contactContext;
    }
    public ContactModel Create(ContactModel contact)
    {
        _ContactContext.Contacts.Add(contact);
        _ContactContext.SaveChanges();
        return contact;
    }

    public async Task<ICollection<ContactModel>> GetAllAsync()
    {
        return await _ContactContext.Contacts.ToListAsync();
    }

    public async Task<ContactModel?> GetAsync(int id)
    {
        return await _ContactContext.Contacts.FirstOrDefaultAsync(contact => contact.Id == id)!;
    }

    public async Task<ContactModel> UpdateAsync(ContactModel contactBody)
    {
        ContactModel? contact = await GetAsync(contactBody.Id)!;

        if (contact is null)
        {
            throw new Exception("Houve um erro na atualização do contato!");
        }

        contact.Name = contactBody.Name;
        contact.Email = contactBody.Email;
        contact.Phone = contact.Phone;
        _ContactContext.SaveChanges();
        return contact;
    }

    public ContactModel Delete(ContactModel contact)
    {
        if (contact is not null)
        {
            _ContactContext.Contacts.Remove(contact);
            _ContactContext.SaveChanges();
            return contact;
        }
        throw new Exception("contact dosent exist for delete");
    }

    public ContactModel GetBy(Func<ContactModel, bool> predicate)
    {
        return _ContactContext.Contacts.FirstOrDefault(predicate)!;
    }
}
