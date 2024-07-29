using System.Net;
using System.Net.Mail;

namespace Aspnet_mvc.Helpers;


public class Email : IEmail
{
    private readonly IConfiguration _configuration;

    public Email(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string Send(string email, string subject, string message)
    {
        try
        {
            string host = _configuration.GetValue<string>("SMTP:Host")!;
            string name = _configuration.GetValue<string>("SMTP:Name")!;
            string from = _configuration.GetValue<string>("SMTP:Email")!;
            string password = _configuration.GetValue<string>("SMTP:Password")!;
            int port = _configuration.GetValue<int>("SMTP:Port");

            MailMessage mail = new()
            {
                From = new MailAddress(from, name),
            };
            mail.To.Add(email);
            mail.Subject = subject;
            mail.Body = message;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            using (SmtpClient smpt = new(host, port))
            {
                smpt.Credentials = new NetworkCredential(from, password);
                smpt.EnableSsl = true;
                smpt.Send(mail);

                return "true";
            }
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }
}
