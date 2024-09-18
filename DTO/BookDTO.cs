namespace DTO;

public class BookDTO
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
    public DateTime DateRelase { get; set; }
    public bool IsActived { get; set; } = true;
}