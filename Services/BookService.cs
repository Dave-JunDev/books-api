using Context;
using Model;
using Microsoft.EntityFrameworkCore;
using Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace Services;

public class BookService : IBookService
{
    private readonly PostgresContext _context;
    public BookService(PostgresContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetAllBooksAsync() 
        => await _context.Books.ToListAsync();
    
    public async Task<Book> GetBookById(int id)
    {
        Book? book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
            return new();

        return book;
    }

    public async Task<List<Book>> GetAllBooksByIds(List<int> ids)
    {
        return await _context.Books.Where(b => ids.Contains(b.Id)).ToListAsync();
    }

    public async Task<Book> CreateBook(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<Book> UpdateBook(int id, Book book)
    {
        Book oldBook = await GetBookById(id);

        if (oldBook.Id == 0)
            return oldBook;

        oldBook.Author = book.Author;
        oldBook.Description = book.Description;
        oldBook.DateRelase = book.DateRelase;
        oldBook.Name = book.Name;

        _context.Books.Update(oldBook);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task DeleteBook(Book book)
    {
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }
}
