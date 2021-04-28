using BookDirectory.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookDirectory.Data.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable(nameof(Book));

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd().IsRequired();

            builder.Property(b => b.Title).IsRequired();

            builder.Property(b => b.Author).IsRequired();
            
            builder.Property(b => b.YearWritten).IsRequired(false);
            
            builder.Property(b => b.Edition).IsRequired(false);
            
            builder.Property(b => b.Price).IsRequired(false);
    }
    }
}
