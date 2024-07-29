namespace Aspnet_mvc.Helpers
{
    public interface IEmail
    {
        string Send(string email, string subject, string message);
    }
}
