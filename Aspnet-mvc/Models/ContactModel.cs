using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Aspnet_mvc.Models;

public class ContactModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Digite o nome do contato")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Digite o email do contato")]
    [EmailAddress(ErrorMessage = "Email invalido")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Digite o phone do contato")]
    public string? Phone { get; set; }
    public bool? IsActive = true;
}
