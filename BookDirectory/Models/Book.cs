using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookDirectory.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public ushort? YearWritten { get; set; }
        public string? Edition { get; set; }
        public double? Price { get; set; }
    }
}
