namespace Aspnet_mvc.Repository;

using Aspnet_mvc.Data;
using Aspnet_mvc.Models;
public class UserRepository : IUserRepository
{
    private readonly ContactContext _Context;
    public UserRepository(ContactContext context)
    {
        _Context = context;
    }
    public UserModel Create(UserModel user)
    {
        user.Created_at = DateTime.Now;
        user.HashPassword();
        _Context.Users.Add(user);
        _Context.SaveChanges();
        return user;
    }

    public ICollection<UserModel> ListAll()
    {
        return _Context.Users.ToList();
    }

    public UserModel Get(int Id)
    {
        return _Context.Users.FirstOrDefault(user => user.Id == Id)!;
    }

    public UserModel Update(UserModel userParam)
    {
        UserModel user = Get(userParam.Id);

        if (user is null)
        {
            throw new Exception("Houve um erro na atualização do contato!");
        }
        userParam.HashPassword();
        user.Email = userParam.Email;
        user.Name = userParam.Name;
        user.Login = userParam.Login;
        user.Profil = userParam.Profil;
        user.Password = userParam.Password;
        user.Updated_at = DateTime.Now;

        _Context.SaveChanges();

        return user;
    }

    public UserModel Delete(UserModel user)
    {
        if (user is null)
        {
            throw new Exception("Não foi possivel deletar o usuario");
        }

        _Context.Users.Remove(user);
        _Context.SaveChanges();
        return user;
    }

    public UserModel GetByLogin(string login)
    {
        return _Context.Users.FirstOrDefault(us => string.Equals(us.Login.ToUpper(), login.ToUpper()))!;
        
    }
}
