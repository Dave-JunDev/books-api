using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model;

[Table("borrowedbooks")]
public class BorrowedBooks
{
    [Key, Column("id")]
    public int Id { get; set; }
    [Column("user")]
    public User? User { get; set; }
    [Column("books")]
    public List<Book>? Books { get; set; }
    [Column("date_borrowed")]
    public DateTime DateBorrowed { get; set; } = DateTime.Now;
    [Column("return_date")]
    public DateTime? ReturnDate { get; set; }
    [Column("UserId")]
    public virtual int UserId { get; set; }
}