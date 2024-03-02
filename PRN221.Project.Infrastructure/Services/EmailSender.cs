using Microsoft.AspNetCore.Identity.UI.Services;

namespace PRN221.Project.Infrastructure.Services;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        return Task.CompletedTask;
    }
}