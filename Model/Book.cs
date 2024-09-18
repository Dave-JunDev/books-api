using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model;

[Table("book")]
public class Book
{
    [Key, Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string? Name { get; set; }
    [Column("description")]
    public string? Description { get; set; }
    [Column("author")]
    public string? Author { get; set; }
    [Column("daterelase")]
    public DateTime DateRelase { get; set; }
    [Column("isactived")]
    public bool IsActived { get; set; } = true;
}