using Aspnet_mvc.Models;

namespace Aspnet_mvc.Repository;

public interface IContactRepository
{
    ContactModel Create(ContactModel contact);
    Task<ICollection<ContactModel>> GetAllAsync();
    Task<ContactModel> GetAsync(int id);
    Task<ContactModel> UpdateAsync(ContactModel contact);
    ContactModel Delete(ContactModel contact);
    ContactModel GetBy(Func<ContactModel, bool> predicate);
}
