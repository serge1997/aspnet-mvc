using System.ComponentModel.DataAnnotations;

namespace Aspnet_mvc.Models;

public class ResetPasswordModel
{
    [Required(ErrorMessage = "Digite o login")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Digite o e-mail")]
    public string Email { get; set; }
}
