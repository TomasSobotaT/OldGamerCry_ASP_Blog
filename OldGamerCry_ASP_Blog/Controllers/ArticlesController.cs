using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OldGamerCry_ASP_Blog.Data;
using OldGamerCry_ASP_Blog.Models;
using pokusoblog2.Models;

namespace OldGamerCry_ASP_Blog.Controllers
{

    //only role "administrator" is allowed use this controller, allowed actions are marked: [AllowAnonymous]
    [Authorize(Roles = "administrator")]
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Articles
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
              return View(await _context.Article.ToListAsync());
        }

        // GET: Articles/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Article == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Content")] Article article, [FromForm] IFormFile file)
        {
            // if image file is not loaded, remove from Modelstate
            if (file == null)
            {
                ModelState.Remove("file");
            }
            

            if (ModelState.IsValid)
            {
                if (file != null)
                {   // if image is loaded, it will be converted
                    using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                    {
                        article.ImageByte = binaryReader.ReadBytes((int)file.Length);
                        article.SaveImageAndMapPath();
                    }
                }



                _context.Add(article);
                await _context.SaveChangesAsync();

                //info email will be send to registered users  after save the new article
                MailManager mailManager = new MailManager(_context);

                try
                {
                    mailManager.SendEmails(article.Title, article.Description, article.Id);
                }
                catch(Exception ex)
                {
                    throw new Exception("Article has been saved but there was an error within sending information emails. Error:");
                }

                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }
     
        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Article == null)
            {
                return NotFound();
            }

            var article = await _context.Article.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Content")] Article article)
        {
            
            if (id != article.Id)
            {
                return NotFound();
            }      
          
            if (ModelState.IsValid)
            {            
                try
                {
                    //only Title, Describtion and Context will be updated
                    var existingArticle = await _context.Article.FindAsync(id);
                    if (existingArticle == null)
                    {
                        return NotFound();
                    }
                    existingArticle.Title = article.Title;
                    existingArticle.Description = article.Description;
                    existingArticle.Content = article.Content;
                    _context.Update(existingArticle);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Article == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Article == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Article'  is null.");
            }
            var article = await _context.Article.FindAsync(id);
            if (article != null)
            {
                _context.Article.Remove(article);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
          return _context.Article.Any(e => e.Id == id);
        }
    }
}
