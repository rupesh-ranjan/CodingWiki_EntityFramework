// See https://aka.ms/new-console-template for more information
using CodingWiki_DataAccess.Data;
using CodingWiki_Model.Models;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

//using(ApplicationDbContext context = new ApplicationDbContext())
//{
//    context.Database.EnsureCreated();
//    if (context.Database.GetPendingMigrations().Count() > 0 )
//    {
//        context.Database.Migrate();
//    }
//}


//AddBook();
//GetBook();
//GetAllBooks();
//UpdateBook();
//DeleteBooK();


//void DeleteBooK()
//{
//    try
//    {
//        using var context = new ApplicationDbContext();
//        var book = context.Books.Find(8);
//        //if (book == null) return;
//        context.Books.Remove(book);
//        context.SaveChanges();

//    }
//    catch (Exception ex)
//    {

//    }
//}

//void UpdateBook()
//{
//    try
//    {
//        using var context = new ApplicationDbContext();
//        var book = context.Books.Find(7);
//        if (book == null) return;
//        book.Publisher_Id = 2;
//        context.SaveChanges();

//    }
//    catch (Exception ex)
//    {

//    }
//}

//void GetBook()
//{
//    using var context = new ApplicationDbContext();
//    var input = "Journey from Fail to Pass";
//    //var book = context.Books.First();
//    //var book = context.Books.FirstOrDefault();
//    //var books = context.Books.Where(u => u.Publisher_Id == 3 && u.Price >100).ToList();
//    //var book = context.Books.FirstOrDefault(u => u.Title == input);

//    // Find only used with Primary key
//    //var book = context.Books.Find(10);

//    //var book = context.Books.SingleOrDefault(u => u.Publisher_Id == 3);
//    //var books = context.Books.Where(u => u.ISBN.Contains("54")).ToList();

//    //var books = context.Books.Where(u => u.Price>500).OrderBy(u => u.Title).ThenByDescending(u => u.ISBN).ToList();
//    var books = context.Books.Skip(2).Take(1).ToList();

//    //Console.WriteLine(book?.Title + " - " + book?.ISBN);

//    foreach (var book in books)
//    {
//        Console.WriteLine(book.Title + " - " + book.ISBN);
//    }
//}



//void GetAllBooks()
//{
//    using var context = new ApplicationDbContext();
//    var books = context.Books.ToList();
//    foreach ( var book in books )
//    {
//        Console.WriteLine(book.Title + " - " + book.ISBN);
//    }
//}

//void AddBook()
//{
//    Book book = new() { Title = "Test New EF Core Book", ISBN = "TEST", Price = 105.95m, Publisher_Id = 1 };
//    using var context = new ApplicationDbContext();
//    var books = context.Books.Add(book);
//    context.SaveChanges();
//}
