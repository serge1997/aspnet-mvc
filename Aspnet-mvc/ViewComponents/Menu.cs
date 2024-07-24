namespace Aspnet_mvc.ViewComponents;

using Aspnet_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
public class Menu : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        string authSession = HttpContext.Session.GetString("authSession")!;
        if (string.IsNullOrEmpty(authSession)) return null!;
        
        UserModel auth = JsonConvert.DeserializeObject<UserModel>(authSession)!;
        return View(auth);
    }
}
