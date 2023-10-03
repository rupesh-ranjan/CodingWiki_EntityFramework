using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodingWiki_Web.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthorController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<Author> authors = _dbContext.Authors.ToList();
            return View(authors);
        }
        public IActionResult Upsert(int? id)
        {
            Author author = new Author();
            if (id == null || id == 0) { return View(author); }
            author = _dbContext.Authors.FirstOrDefault(u => u.Author_Id == id);
            if (author == null) return NotFound();
            return View(author);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Author author)
        {
            if (ModelState.IsValid)
            {
                if (author.Author_Id == 0)
                {
                    _dbContext.Authors.Add(author);
                }
                else
                {
                    _dbContext.Authors.Update(author);
                }
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }
        public IActionResult Delete(int id)
        {
            Author author = _dbContext.Authors.FirstOrDefault(u => u.Author_Id == id);
            if (author == null) return NotFound();
            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
