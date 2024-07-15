using HotelWebDemo.Configuration;
using HotelWebDemo.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace HotelWebDemo.Services;

public class MailingService : IMailingService
{
    public readonly MailSettings mailSettings;
    private readonly IViewRenderService viewRenderService;

    public MailingService(IOptions<MailSettings> options, IViewRenderService viewRenderService)
    {
        mailSettings = options.Value;
        this.viewRenderService = viewRenderService;
    }

    public bool SendMail(Mail mail)
    {
        try
        {
            MimeMessage emailMessage = new();
            MailboxAddress emailFrom = new(mailSettings.Name, mailSettings.EmailId);
            emailMessage.From.Add(emailFrom);

            MailboxAddress emailTo = new(mail.EmailToName, mail.EmailToId);
            emailMessage.To.Add(emailTo);

            emailMessage.Subject = mail.EmailSubject;

            BodyBuilder emailBodyBuilder = new();
            emailBodyBuilder.HtmlBody = mail.EmailBody;
            emailMessage.Body = emailBodyBuilder.ToMessageBody();

            SmtpClient MailClient = new();
            MailClient.Connect(mailSettings.Host, mailSettings.Port, mailSettings.UseSSL);
            MailClient.Authenticate(mailSettings.UserName, mailSettings.Password);
            MailClient.Send(emailMessage);
            MailClient.Disconnect(true);
            MailClient.Dispose();

            return true;
        }
        catch (Exception)
        {
            // Log exception
            return false;
        }
    }

    public async Task<bool> SendMail(string subject, string email, string emailTemplate, object model)
    {
        string html = await viewRenderService.RenderToStringAsync(emailTemplate, model);

        Mail mail = new()
        {
            EmailToId = email,
            EmailBody = html,
            EmailSubject = subject,
            EmailToName = "Name"
        };

        return SendMail(mail);
    }
}
