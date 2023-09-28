using CodingWiki_Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost; Database=CodingWiki; TrustServerCertificate=True; Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().Property(u => u.Price).HasPrecision(10, 5);

            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "Down to Dusk", ISBN = "FF56SF5811", Price = 408.96m },
                new Book { BookId = 2, Title = "Rise and Fall", ISBN = "21516CSD41", Price = 805.33m }
                );
            var bookList = new Book[]
            {
                new Book { BookId = 3, Title = "The Story of Truth", ISBN = "5DFVDDF4", Price = 586.56m },
                new Book { BookId = 4, Title = "Journey from Fail to Pass", ISBN = "54F4DF5", Price = 455.55m },
                new Book { BookId = 5, Title = "Dusky Night", ISBN = "DF5G484DF54", Price = 285.56m }

            };
            modelBuilder.Entity<Book>().HasData(bookList);
        }
    }
}
