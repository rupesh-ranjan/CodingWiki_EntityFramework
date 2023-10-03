using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodingWiki_Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<Category> categories = _dbContext.Categories.ToList();
            return View(categories);
        }
        public IActionResult Upsert(int? id)
        {
            Category category = new Category();
            if (id == null || id == 0) return View(category);
            category = _dbContext.Categories.FirstOrDefault(u => u.CategoryId == id);
            if (category == null) return NotFound();

            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.CategoryId == 0)
                {
                    // Create
                    _dbContext.Categories.Add(category);
                } 
                else
                {
                    // Update
                    _dbContext.Categories.Update(category);
                }
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }
        public IActionResult Delete(int id)
        {
            Category category = new Category();
            category = _dbContext.Categories.FirstOrDefault(u => u.CategoryId == id);
            if (category == null) return NotFound();
            _dbContext.Remove(category);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult CreateMultiple2()
        {
            List<Category> categories = new List<Category>();
            for (int i = 1; i <=2; i++)
            {
                categories.Add(new Category { CategoryName = Guid.NewGuid().ToString()});
            }
            _dbContext.Categories.AddRange(categories);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateMultiple5()
        {
            List<Category> categories = new List<Category>();
            for (int i = 1; i <=5; i++)
            {
                categories.Add(new Category { CategoryName = Guid.NewGuid().ToString()});
            }
            _dbContext.Categories.AddRange(categories);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult RemoveMultiple2()
        {
            List<Category> categories = _dbContext.Categories.OrderByDescending(u => u.CategoryId).Take(2).ToList();
            _dbContext.Categories.RemoveRange(categories);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult RemoveMultiple5()
        {
            List<Category> categories = _dbContext.Categories.OrderByDescending(u => u.CategoryId).Take(5).ToList();
            _dbContext.Categories.RemoveRange(categories);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
