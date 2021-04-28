using System;
using System.ComponentModel.DataAnnotations;

namespace BookDirectory.Dtos
{
    public record UpdateBookDto
    {
        [Required(ErrorMessage = "Please provide the ID of the book")]
        public Guid Id { get; set; }
        
        public string? Title { get; init; }
        public string? Author { get; init; }
        public ushort? YearWritten { get; init; }
        public string? Edition { get; init; }
        public double? Price { get; init; }
    }    
}
