using ViewToStringNMailKit.Models;

namespace ViewToStringNMailKit.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);
    }
}
