using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Author must be between 2 and 100 characters")]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "ISBN is required")]
        [RegularExpression(@"^(?:\d{10}|\d{13})$", ErrorMessage = "ISBN must be 10 or 13 digits")]
        public string ISBN { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Publisher cannot exceed 100 characters")]
        public string Publisher { get; set; } = string.Empty;

        [Range(1000, 2100, ErrorMessage = "Publication year must be between 1000 and 2100")]
        public int PublicationYear { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters")]
        public string Category { get; set; } = string.Empty;

        [Range(1, 1000, ErrorMessage = "Total copies must be between 1 and 1000")]
        public int TotalCopies { get; set; }

        [Range(0, 1000, ErrorMessage = "Available copies must be between 0 and 1000")]
        public int AvailableCopies { get; set; }

        public bool IsAvailable { get; set; }
    }
}