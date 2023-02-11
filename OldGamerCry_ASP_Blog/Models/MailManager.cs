using Microsoft.EntityFrameworkCore;
using OldGamerCry_ASP_Blog.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Net.Mail;
namespace OldGamerCry_ASP_Blog.Models
{
    [NotMapped]
    public class MailManager
    {
        private readonly ApplicationDbContext _context;
    

        public MailManager(ApplicationDbContext context) 
        {
            _context = context;
        }
        
        //public void SendEmails() 
        //{
        //    var email = _context.Article.Select(a=>a.)
        
        //}


    }
}
