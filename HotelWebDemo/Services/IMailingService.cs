using HotelWebDemo.Models;

namespace HotelWebDemo.Services;

public interface IMailingService
{
    bool SendMail(Mail mail);

    Task<bool> SendMail(string subject, string email, string emailTemplate, object model);
}
