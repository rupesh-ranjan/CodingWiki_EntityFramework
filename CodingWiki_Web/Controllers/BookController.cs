using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using CodingWiki_Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CodingWiki_Web.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public BookController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<Book> books = _dbContext.Books.Include(u => u.Publisher).Include(u => u.BookAuthorMap).ThenInclude(u => u.Author).ToList();

            //List<Book> books = _dbContext.Books.ToList();
            //foreach (var book in books)
            //{
            //    //    // Least Efficient
            //    //    //book.Publisher = _dbContext.Publishers.FirstOrDefault(u => u.Publisher_Id == book.Publisher_Id);

            //    //    // More Efficient  n+1 loading
            //    _dbContext.Entry(book).Reference(u => u.Publisher).Load();
            //    _dbContext.Entry(book).Collection(u => u.BookAuthorMap).Load();
            //    foreach (var bookAuthor in book.BookAuthorMap)
            //    {
            //        _dbContext.Entry(bookAuthor).Reference(u => u.Author).Load();
            //    }
            //}
            return View(books);
        }
        public IActionResult Upsert(int? id)
        {
            BookVM bookVM = new BookVM();
            bookVM.PublisherList = _dbContext.Publishers.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Publisher_Id.ToString()
            });
            if (id == null || id == 0)
            {
                // Create
                return View(bookVM);
            }
            // Edit
            bookVM.Book = _dbContext.Books.FirstOrDefault(u => u.BookId == id);
            if (bookVM == null) return NotFound();

            return View(bookVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(BookVM bookVM)
        {
            if (bookVM.Book.BookId == 0)
            {
                // Create
                _dbContext.Books.Add(bookVM.Book);
            }
            else
            {
                // Update
                _dbContext.Books.Update(bookVM.Book);
            }
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            BookDetail bookDetail = _dbContext.BookDetails.Include(u => u.Book).FirstOrDefault(u => u.Book_Id == id);
            if (bookDetail == null) bookDetail  = new BookDetail();
            bookDetail.Book = _dbContext.Books.FirstOrDefault(u => u.BookId == id);
            bookDetail.Book_Id = (int)id;
            //if (bookDetail == null) return NotFound();

            return View(bookDetail);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(BookDetail bookDetail)
        {
            if (bookDetail.BookDetail_Id == 0)
            {
                // Create
                _dbContext.BookDetails.Add(bookDetail);
            }
            else
            {
                // Update
                _dbContext.BookDetails.Update(bookDetail);
            }
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Book book = new Book();
            book = _dbContext.Books.FirstOrDefault(u => u.BookId == id);
            if (book == null) return NotFound();
            _dbContext.Remove(book);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult ManageAuthors(int id)
        {
            BookAuthorVM bookAuthorVM = new BookAuthorVM()
            {
                BookAuthorList = _dbContext.BookAuthorMaps.Include(u => u.Author).Include(u => u.Book).Where(u => u.Book_Id == id)
                .Where(u => u.Book_Id == id).ToList(),
                BookAuthor = new()
                {
                    Book_Id = id
                },
                Book = _dbContext.Books.FirstOrDefault(u => u.BookId ==id) 

            };
            List<int> tempListOfAssignedAuthor = bookAuthorVM.BookAuthorList.Select(u => u.Author_Id).ToList();

            // Not in class
            // Get all the authors whose id is not in tempListOfAssignedAuthor

            var tempList = _dbContext.Authors.Where(u => !tempListOfAssignedAuthor.Contains(u.Author_Id)).ToList();
            bookAuthorVM.AuthorList = tempList.Select(i => new SelectListItem
            {
                Text = i.FullName,
                Value = i.Author_Id.ToString()
            });
            return View(bookAuthorVM);
        }

        [HttpPost]
        public IActionResult ManageAuthors(BookAuthorVM bookAuthorVM)
        {
            if (bookAuthorVM.BookAuthor.Book_Id != 0 && bookAuthorVM.BookAuthor.Author_Id != 0)
            {
                _dbContext.BookAuthorMaps.Add(bookAuthorVM.BookAuthor);
                _dbContext.SaveChanges();
            }
            
            return RedirectToAction(nameof(ManageAuthors), new { @id = bookAuthorVM.BookAuthor.Book_Id});
        }

        [HttpPost]
        public IActionResult RemoveAuthors(BookAuthorVM bookAuthorVM, int authorId)
        {
            int bookId = bookAuthorVM.Book.BookId;
            BookAuthorMap bookAuthorMap = _dbContext.BookAuthorMaps.FirstOrDefault(
                u => u.Author_Id == authorId && u.Book_Id== bookId);
            _dbContext.BookAuthorMaps.Remove(bookAuthorMap);
            _dbContext.SaveChanges();
            
            return RedirectToAction(nameof(ManageAuthors), new { @id = bookId });
        }

        public IActionResult PlayGround()
        {
            var bookDetail1 = _dbContext.BookDetails.Include(u => u.Book).FirstOrDefault(u => u.Book_Id == 12);
            bookDetail1.NumberOfChapters = "11";
            bookDetail1.Book.Price = 11;
            _dbContext.BookDetails.Update(bookDetail1);
            _dbContext.SaveChanges();


            var bookDetail2 = _dbContext.BookDetails.Include(u => u.Book).FirstOrDefault(u => u.Book_Id == 12);
            bookDetail2.NumberOfChapters = "22";
            bookDetail2.Book.Price = 22;
            _dbContext.BookDetails.Attach(bookDetail2);
            _dbContext.SaveChanges();

            //IEnumerable<Book> bookList1 = _dbContext.Books;
            //var filteredBook1 = bookList1.Where(u => u.Price > 200).ToList();


            //IQueryable<Book> bookList2 = _dbContext.Books;
            //var filteredBook2 = bookList2.Where(u => u.Price > 200).ToList();


            //var bookTemp = _dbContext.Books.FirstOrDefault();
            //bookTemp.Price = 100;

            //var bookCollection = _dbContext.Books;
            //decimal totalPrice = 0;

            //foreach (var book in bookCollection)
            //{
            //    totalPrice += book.Price;
            //}

            //var bookList = _dbContext.Books.ToList();
            //foreach (var book in bookList)
            //{
            //    totalPrice += book.Price;
            //}

            //var bookCollection2 = _dbContext.Books;
            //var bookCount1 = bookCollection2.Count();

            //var bookCount2 = _dbContext.Books.Count();


            return RedirectToAction(nameof(Index));
        }
    }
}
