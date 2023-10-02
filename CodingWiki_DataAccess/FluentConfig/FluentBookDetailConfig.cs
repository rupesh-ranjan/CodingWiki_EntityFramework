using CodingWiki_Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_DataAccess.FluentConfig
{
    public class FluentBookDetailConfig : IEntityTypeConfiguration<Fluent_BookDetail>
    {
        public void Configure(EntityTypeBuilder<Fluent_BookDetail> modelBuilder)
        {
            // Name of Table
            modelBuilder.ToTable("Fluent_BookDetails");

            // Name of Columns
            modelBuilder.Property(u => u.NumberOfChapters).HasColumnName("NoOfChapters");

            //Priary Key
            modelBuilder.HasKey(u => u.BookDetail_Id);

            // Other Validations
            modelBuilder.Property(u => u.NumberOfChapters).IsRequired();

            // Relations
            modelBuilder.HasOne(b => b.Book).WithOne(b => b.BookDetail)
                .HasForeignKey<Fluent_BookDetail>(u => u.Book_Id);
        }
    }
}
