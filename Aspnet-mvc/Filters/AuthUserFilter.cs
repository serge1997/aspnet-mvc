namespace Aspnet_mvc.Filters;

using Aspnet_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

public class AuthUserFilter : ActionFilterAttribute
{

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        string authSession = context.HttpContext.Session.GetString("authSession")!;

        if (string.IsNullOrEmpty(authSession))
        {
            context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login"}, {"action", "Index" } });
        }
        else
        {
            UserModel user = JsonConvert.DeserializeObject<UserModel>(authSession)!;

            if (user is null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
            }
        }
        base.OnActionExecuted(context);
    }
}
