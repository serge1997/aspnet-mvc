namespace Aspnet_mvc.Repository;

using Aspnet_mvc.Models;
public interface IUserRepository
{
    UserModel Create(UserModel user);
    ICollection<UserModel> ListAll();
    UserModel Get(int Id);
    UserModel Update(UserModel user);
    UserModel Delete(UserModel user);
    UserModel GetByLogin(string login);
    UserModel GetBy(Func<UserModel, bool> predicate);

}
