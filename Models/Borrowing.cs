using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Borrowing
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Book ID is required")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Reader ID is required")]
        public int ReaderId { get; set; }

        public DateTime BorrowDate { get; set; } = DateTime.UtcNow;     

        public DateTime? ReturnDate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [RegularExpression(@"^(Active|Returned|Overdue)$", ErrorMessage = "Status must be Active, Returned, or Overdue")]
        public string Status { get; set; } = "Active";

        public int OverdueDays { get; set; }              

    }
}