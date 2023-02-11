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
        public void SendEmails(string title, string description)
        {
            var users = _context.Users.ToList();

            MailAddress fromAddress = new MailAddress("mailnapokusy123@gmail.com","OldGamerCry - Blog");
            MailMessage mailMessage = new MailMessage();

            mailMessage.From = fromAddress;
            mailMessage.Subject = "New Article";
            mailMessage.Body = title + description;
          

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
