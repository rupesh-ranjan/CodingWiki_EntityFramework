using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodingWiki_Web.Controllers
{
    public class PublisherController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public PublisherController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<Publisher> publishers = _dbContext.Publishers.ToList();
            return View(publishers);
        }
        public IActionResult Upsert(int? id)
        {
            Publisher publisher = new Publisher();
            if (id == null || id == 0) { return View(publisher); }
            publisher = _dbContext.Publishers.FirstOrDefault(u => u.Publisher_Id == id);
            if (publisher == null) return NotFound();

            return View(publisher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                if (publisher.Publisher_Id == 0)
                {
                    // Create
                    _dbContext.Publishers.Add(publisher);
                }
                else
                {
                    // Update
                    _dbContext.Publishers.Update(publisher);
                }
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(publisher);
        }
        public IActionResult Delete(int id)
        {
            Publisher publisher = _dbContext.Publishers.FirstOrDefault(u => u.Publisher_Id == id);
            if (publisher == null) return NotFound();
            _dbContext.Publishers.Remove(publisher);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
