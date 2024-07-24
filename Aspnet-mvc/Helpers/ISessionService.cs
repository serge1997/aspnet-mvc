using Aspnet_mvc.Models;

namespace Aspnet_mvc.Helpers;

public interface ISessionService
{
    void CreateUserSession(UserModel user);
    void RemoveUserSession();
    UserModel GetUserSession();
}
