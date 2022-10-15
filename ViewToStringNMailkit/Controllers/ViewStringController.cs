using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ViewToStringNMailKit.Services.ViewRender;
using ViewToStringNMailKit.Models;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using static Org.BouncyCastle.Math.EC.ECCurve;
using Microsoft.Extensions.Configuration;
using MailKit.Net.Smtp;

namespace ViewToStringNMailKit.Controllers
{
    public class ViewStringController : Controller
    {
        private readonly IViewRendererService  _renderer;
        private readonly IConfiguration _config;
        public ViewStringController(IViewRendererService  renderer, IConfiguration config)
        {
            _renderer = renderer;
            _config = config;
        }
        public IActionResult Index()
        {
            List<Employee> employees = GetEmployees();
            return View(employees);
        }

        public async Task<IActionResult> EmployeeHtmlEmail()
        {
            var emailHtmlFromView = await this._renderer.RenderAsync(this, "Index", GetEmployees());

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EtherealSMTP:EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse("kristin25@ethereal.email"));
            email.Subject = "Test";
            email.Body = new TextPart(TextFormat.Html) { Text =emailHtmlFromView };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EtherealSMTP:EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EtherealSMTP:EmailUsername").Value, _config.GetSection("EtherealSMTP:EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);

            return new OkObjectResult(emailHtmlFromView);
        }

        private List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee { EmployeeId = 1, FirstName = "Raj", LastName = "Mah", Email = "test@test.com" });
            employees.Add(new Employee { EmployeeId = 2, FirstName = "Raj2", LastName = "Mah2", Email = "test2@test.com" });
            employees.Add(new Employee { EmployeeId = 3, FirstName = "Raj3", LastName = "Mah3", Email = "test3@test.com" });
            return employees;
        }
    }
}
