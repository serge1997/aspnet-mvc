using Aspnet_mvc.Data;
using Aspnet_mvc.Models;

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

    public ICollection<ContactModel> GetAll()
    {
        return _ContactContext.Contacts.ToList();
    }

    public ContactModel Get(int id)
    {
        return _ContactContext.Contacts.FirstOrDefault(contact => contact.Id == id)!;
    }

    public ContactModel Update(ContactModel contactBody)
    {
        ContactModel contact = Get(contactBody.Id);

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
