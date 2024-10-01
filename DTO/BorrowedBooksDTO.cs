
namespace DTO;

public class BorrowedBooksDTO
{
    public int UserId { get; set; }
    public List<int>? BookIds { get; set; }
    public DateTime DateBorrowed { get; set; } = DateTime.Now;
    public DateTime? ReturnDate { get; set; }
}