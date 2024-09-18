using Model;

namespace Interfaces;

public interface IBookService
{
    Task<List<Book>> GetAllBooksAsync();
    Task<Book> GetBookById(int id);
    Task<Book> CreateBook(Book book);
    Task<Book> UpdateBook(int id, Book book);
    Task DeleteBook(Book book);
}