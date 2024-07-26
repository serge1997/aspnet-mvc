namespace Aspnet_mvc.Models;

using Aspnet_mvc.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Aspnet_mvc.Helpers;

public class UserModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int Id { get; set; }
    [Required(ErrorMessage = "Digite o nome")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Digite o login")]
    public string Login { get; set; }
    [Required(ErrorMessage = "Digite o email")]
    [EmailAddress(ErrorMessage = "Formato do email é invalido")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Escolhe o perfil do usuario")]
    public ProfilEnum Profil { get; set; }
    [Required(ErrorMessage = "Digite a senha")]
    public string Password { get; set; }
    public DateTime Created_at {  get; set; }
    public DateTime? Updated_at { get;set; }


    public string getProfil()
    {
        return Profil == 0 ? "Padrão" : "Admin";
    }

    public bool passwordIsValid(string password)
    {
        return string.Equals(Password, password.Crypt());
    }

    public void HashPassword()
    {
        Password = Password.Crypt();
    }
}
