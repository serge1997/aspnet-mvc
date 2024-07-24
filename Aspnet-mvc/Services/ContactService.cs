namespace Aspnet_mvc.Services;

using Aspnet_mvc.Models;
using Aspnet_mvc.Repository;
public class ContactService
{

    public static bool VeirifyEmail(IContactRepository repository, string email)
    {
        ContactModel contact = repository
            .GetBy(cont => string.Equals(cont.Email, email, StringComparison.OrdinalIgnoreCase));

        return contact is not null ? true : false;
    }
}