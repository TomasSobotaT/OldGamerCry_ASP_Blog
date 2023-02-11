using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OldGamerCry_ASP_Blog.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Net.Mail;

namespace OldGamerCry_ASP_Blog.Models
{/// <summary>
/// Class to send mail all registered users
/// </summary>
    [NotMapped]
    
    public class MailManager : Controller
    {
        private readonly ApplicationDbContext _context;
    

        public MailManager(ApplicationDbContext context) 
        {
            _context = context;
        }




        /// <summary>
        /// Send mails to all registered users
        /// </summary>
        /// <param name="title">title of article</param>
        /// <param name="description">description of article</param>
        public void SendEmails(string title, string description,int id)
        {
            var users = _context.Users.ToList();

            MailAddress fromAddress = new MailAddress("mailnapokusy123@gmail.com","OldGamerCry - Blog");
            MailMessage mailMessage = new MailMessage();

            string lastArticleUrl = $"https://localhost:7046/Articles/Details/{id.ToString()}";

            mailMessage.From = fromAddress;
            mailMessage.Subject = "New Article";
            mailMessage.IsBodyHtml = true;

            mailMessage.Body =
            "<html>" +
            "<head>" +
            "</head>" +
            "<body>" +
            "<section style=\"border:20px solid rgba(88,143,39,255); padding: 20px; margin: 30px; border-radius:15px; text-align: center;\">" +
            "<h1>Hi,</h1>" +
            "<p>new article has been added to blog: </p> <br>" +
            $"<h2 style=\"color:rgba(88,143,39,255);\">{title}</h2>" +
            $"<h4 style=\"color:rgba(88,143,39,255);\">{description}</h4> <br>" +
            $"<p>You can read it <a style=\"color:blue;\" href=" + lastArticleUrl + ">here</a> </p>" +
            "<br> <br>" +
            $"<p>OldGamerCry Blog Team <br>Tomas Sobota <br> Prague {DateTime.Now.ToString("yyyy")}</p>" +
            "</section>" +
            "</body>" +
            "</html>";

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("mailnapokusy123@gmail.com", "mpdlrqetkpbgxwty");

            //sending mails one by one, not group
            foreach (var user in users)
            {
                mailMessage.To.Clear();
                mailMessage.To.Add(user.Email);
                smtpClient.Send(mailMessage);
            }


        }


    }
}
