using Aspnet_mvc.Models;
using Newtonsoft.Json;

namespace Aspnet_mvc.Helpers;

public class SessionService : ISessionService
{
    private readonly IHttpContextAccessor _HttpcontextAccessor;

    public SessionService(IHttpContextAccessor httpContextAccessor)
    {
        _HttpcontextAccessor = httpContextAccessor;
    }

    public void CreateUserSession(UserModel user)
    {
        string model = JsonConvert.SerializeObject(user);
        _HttpcontextAccessor.HttpContext!.Session.SetString("authSession", model);
    }

    public void RemoveUserSession()
    {
        _HttpcontextAccessor.HttpContext!.Session.Remove("authSession");
    }

    public UserModel GetUserSession()
    {
        string sessionData = _HttpcontextAccessor.HttpContext!.Session.GetString("authSession")!;
        if (string.IsNullOrEmpty(sessionData)) return null!;

        return JsonConvert.DeserializeObject<UserModel>(sessionData)!;
    }
}
