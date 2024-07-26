namespace Aspnet_mvc.Filters;

using Aspnet_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

public class AdminFilter : ActionFilterAttribute
{

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        string auth = context.HttpContext.Session.GetString("authSession")!;

        UserModel user = JsonConvert.DeserializeObject<UserModel>(auth)!;

        if (user.Profil != Enums.ProfilEnum.Admin)
        {
            context.Result = new RedirectToRouteResult(new RouteValueDictionary{ { "controller", "Contact"}, { "action", "Index"} });
        }
        base.OnActionExecuted(context);
    }
}
