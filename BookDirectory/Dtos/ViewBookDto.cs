using System;

namespace BookDirectory.Dtos
{
    public record ViewBookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public ushort? YearWritten { get; set; }
        public string? Edition { get; set; }
        public double? Price { get; set; }
    }
}
