﻿using Aspnet_mvc.Models;
using Aspnet_mvc.Repository;
using Microsoft.AspNetCore.Mvc;
using Aspnet_mvc.Services;
using Aspnet_mvc.Filters;

namespace Aspnet_mvc.Controllers;

[AuthUserFilter]
public class ContactController : Controller
{
    private readonly IContactRepository _contactRepository;
    public ContactController(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<IActionResult> Index()
    {
        ICollection<ContactModel> contacts = await _contactRepository.GetAllAsync();
        return View(contacts);
    }

    public IActionResult Create()
    {
        return View();
    }

    public async Task<IActionResult> Edit(int Id)
    {
        var contact = await _contactRepository.GetAsync(Id);
        return View(contact);
    }

    public IActionResult Delete(int Id)
    {
        var contact = _contactRepository.GetAsync(Id);
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
            _contactRepository.UpdateAsync(model);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAction(ContactModel contact)
    {
        try
        {
            ContactModel deleteContact = await _contactRepository.DeleteAsync(contact);
            TempData["MessageSuccess"] = $"Contato deletado com successo, {deleteContact.Name}";
            return RedirectToAction(nameof(Index), contact);
        }catch(Exception ex)
        {
            TempData["MessageError"] = $"error ocurred to delete {contact.Id}: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }
    

}
