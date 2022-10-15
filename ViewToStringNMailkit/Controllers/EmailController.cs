using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using Org.BouncyCastle.Asn1.Ocsp;
using ViewToStringNMailKit.Models;
using ViewToStringNMailKit.Services.EmailService;

namespace ViewToStringNMailKit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;

        public EmailController(IEmailService emailService, IConfiguration config)
        {
            _emailService = emailService;
            _config = config;
        }

        [HttpPost]
        public IActionResult SendEmail()
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EtherealSMTP:EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse("zraju@hotmail.com"));
            email.Subject = "Test";
            email.Body = new TextPart(TextFormat.Html) { Text = "Test data" };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EtherealSMTP:EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EtherealSMTP:EmailUsername").Value, _config.GetSection("EtherealSMTP:EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
            return Ok();
        }

        [HttpPost]
        public IActionResult SendEmailWithDto(EmailDto emailDtorequest)
        {
            _emailService.SendEmail(emailDtorequest);
            return Ok();
        }
    }
}
