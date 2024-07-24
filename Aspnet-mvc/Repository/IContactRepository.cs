using Aspnet_mvc.Models;

namespace Aspnet_mvc.Repository;

public interface IContactRepository
{
    ContactModel Create(ContactModel contact);
    ICollection<ContactModel> GetAll();
    ContactModel Get(int id);
    ContactModel Update(ContactModel contact);
    ContactModel Delete(ContactModel contact);
    ContactModel GetBy(Func<ContactModel, bool> predicate);
}
