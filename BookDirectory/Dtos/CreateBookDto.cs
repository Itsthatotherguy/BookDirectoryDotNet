using System.ComponentModel.DataAnnotations;

namespace BookDirectory.Dtos
{
    public record CreateBookDto
    {
        [Required(ErrorMessage = "Please provide the title of the book")]
        public string Title { get; init; } = "";

        [Required(ErrorMessage = "Please provide the author of the book")]
        public string Author { get; init; } = "";

        public ushort? YearWritten { get; init; }
        public string? Edition { get; init; }
        public double? Price { get; init; }
    }    
}
