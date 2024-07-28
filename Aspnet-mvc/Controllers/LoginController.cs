using Aspnet_mvc.Helpers;
using Aspnet_mvc.Models;
using Aspnet_mvc.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Aspnet_mvc.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ISessionService _sessionService;

        public LoginController(IUserRepository userRepository,ISessionService sessionService)
        {
            _userRepository = userRepository;
            _sessionService = sessionService;
        }
        public IActionResult Index()
        {
            if (_sessionService.GetUserSession() is not null) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public IActionResult Auth(LoginModel loginParam)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel userLogin = _userRepository.GetByLogin(loginParam.Login);

                    if (userLogin is not null && userLogin.passwordIsValid(loginParam.Password))
                    {
                        _sessionService.CreateUserSession(userLogin);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["MessageError"] = "Senha invalidá";
                    }
                    TempData["MessageError"] = "Login ou senha invalido";
                }
                return View(nameof(Index), loginParam);
            }
            catch(Exception ex)
            {
                TempData["MessageError"] = $"Não foi possvel realizar o login: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Logout()
        {
            _sessionService.RemoveUserSession();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendResetPasswordURL(ResetPasswordModel reset)
        {
           try
            {
                if (ModelState.IsValid)
                {
                    UserModel user = _userRepository.GetBy(user => 
                        string.Equals(user.Email, reset.Email, StringComparison.OrdinalIgnoreCase) && string.Equals(user.Login, reset.Login, StringComparison.OrdinalIgnoreCase));

                    if (user is not null)
                    {
                        TempData["MessageSuccess"] = "User finded";
                        return RedirectToAction(nameof(ResetPassword), reset);
                    }
                }
                return View(nameof(ResetPassword));
            }
            catch (Exception ex)
            {
                TempData["MessageError"] = $"Não foi possivel redefinir a senha {ex.Message}";
                return View(nameof(ResetPassword), reset);
            }
        }
    }
}
