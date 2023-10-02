using CodingWiki_DataAccess.FluentConfig;
using CodingWiki_Model.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingWiki_DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }
        public DbSet<BookAuthorMap> BookAuthorMaps { get; set; }
        public DbSet<Fluent_BookDetail> BookDetails_fluent { get; set; }
        public DbSet<Fluent_Book> Fluent_Books { get; set; }
        public DbSet<Fluent_Publisher> Fluent_Publishers { get; set; }
        public DbSet<Fluent_Author> Fluent_Authors { get; set; }
        public DbSet<Fluent_BookAuthorMap> Fluent_BookAuthorMaps { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost; Database=CodingWiki; TrustServerCertificate=True; Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().Property(u => u.Price).HasPrecision(10, 5);
            modelBuilder.Entity<BookAuthorMap>().HasKey(u => new { u.Book_Id, u.Author_Id });

            modelBuilder.ApplyConfiguration(new FluentAuthorConfig());
            modelBuilder.ApplyConfiguration(new FluentBookAuthorMapConfig());
            modelBuilder.ApplyConfiguration(new FluentBookConfig());
            modelBuilder.ApplyConfiguration(new FluentBookDetailConfig());
            modelBuilder.ApplyConfiguration(new FluentPublisherConfig());

            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "Down to Dusk", ISBN = "FF56SF5811", Price = 408.96m, Publisher_Id = 1 },
                new Book { BookId = 2, Title = "Rise and Fall", ISBN = "21516CSD41", Price = 805.33m, Publisher_Id = 1 }
                );
            var bookList = new Book[]
            {
                new Book { BookId = 3, Title = "The Story of Truth", ISBN = "5DFVDDF4", Price = 586.56m, Publisher_Id = 2 },
                new Book { BookId = 4, Title = "Journey from Fail to Pass", ISBN = "54F4DF5", Price = 455.55m, Publisher_Id = 3 },
                new Book { BookId = 5, Title = "Dusky Night", ISBN = "DF5G484DF54", Price = 285.56m, Publisher_Id = 3 }

            };
            modelBuilder.Entity<Book>().HasData(bookList);
            
            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { Publisher_Id = 1, Name = "Publisher 1", Location = "New Delhi"},
                new Publisher { Publisher_Id = 2, Name = "Publisher 2", Location = "Mumbai"},
                new Publisher { Publisher_Id = 3, Name = "Publisher 3", Location = "Chennai"}
                );
        }
    }
}
