using Aspnet_mvc.Models;
using Aspnet_mvc.Repository;
using Microsoft.AspNetCore.Mvc;
using Aspnet_mvc.Services;

namespace Aspnet_mvc.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;
        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public IActionResult Index()
        {
            var contacts = _contactRepository.GetAll();
            return View(contacts);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int Id)
        {
            ContactModel contact = _contactRepository.Get(Id);
            return View(contact);
        }

        public IActionResult Delete(int Id)
        {
            ContactModel contact = _contactRepository.Get(Id);
            return View(contact);
        }

        [HttpPost]
        public IActionResult Create(ContactModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                  
                    if (ContactService.VeirifyEmail(_contactRepository, model.Email!))
                    {
                        TempData["MessageError"] = $"contact already exist : {model.Email}";
                        return RedirectToAction(nameof(Index));
                    }

                    _contactRepository.Create(model);
                    TempData["MessageSuccess"] = "Contato criado com successo";
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch(Exception ex)
            {
                TempData["MessageError"] = "Não possivel cadastrar o contato. error: " + ex.Message;
                return RedirectToAction("Index");
            }
            
        }
        [HttpPost]
        public IActionResult Edit(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["MessageSuccess"] = $"Contato alterado com successo";
                _contactRepository.Update(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteAction(ContactModel contact)
        {
            try
            {
                ContactModel deleteContact = _contactRepository.Delete(contact);
                TempData["MessageSuccess"] = $"Contato deletado com successo, {deleteContact.Name}";
                return RedirectToAction(nameof(Index), contact);
            }catch(Exception ex)
            {
                TempData["MessageError"] = $"error ocurred to delete {contact.Id}: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
        

    }
}
