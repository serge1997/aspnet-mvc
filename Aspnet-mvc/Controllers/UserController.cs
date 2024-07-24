using Aspnet_mvc.Models;
using Aspnet_mvc.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Reflection.Metadata.Ecma335;

namespace Aspnet_mvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            ICollection<UserModel> users = _userRepository.ListAll();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int Id)
        {
            UserModel user = _userRepository.Get(Id);
            return View(user);
        }

        public IActionResult Delete(int Id)
        {
            UserModel user = _userRepository.Get(Id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Create(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userRepository.Create(user);
                    TempData["MessageSuccess"] = $"Usuario {user.Name} criado com successo";
                    return RedirectToAction(nameof(Index), user);
                }
                return View(nameof(Create));
            }
            catch (Exception ex)
            {
                TempData["MessageError"] = $"Não foi possivel criar um usuario: {ex.Message}";
                return View(nameof(Create));
            }
            
        }

        [HttpPost]
        public IActionResult Edit(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userRepository.Update(user);
                    TempData["MessageSuccess"] = "Usuarion alterado com successo";
                    return RedirectToAction(nameof(Index));
                }
                return View(user);
            }
            catch (Exception ex)
            {
                TempData["MessageError"] = $"Não foi pssivel aletrar `{user.Name} : {ex.Message}";
                return View(user);
            }
        }

        [HttpPost]
        public IActionResult Delete(UserModel user)
        {
            try
            {
                if (user is not null)
                {
                    _userRepository.Delete(user);
                    TempData["MessageSuccess"] = $"Usuario {user.Name} deletado com successo";
                    return RedirectToAction(nameof(Index));
                }
                return View(nameof(Delete));
            }
            catch (Exception ex)
            {
                TempData["MessageSuccess"] = $"Não foi possivel deletar {user.Name}: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
