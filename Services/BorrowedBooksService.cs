
using Context;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Services;

public class BorrowedBooksService : ICommonService<BorrowedBooks>
{
    private readonly PostgresContext _context;
    public BorrowedBooksService(PostgresContext context)
    {
        _context = context;
    }

    public async Task<BorrowedBooks> CreateAsync(BorrowedBooks entity)
    {
        await _context.BorrowedBooks.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<BorrowedBooks> DeleteAsync(BorrowedBooks entity)
    {
        _context.BorrowedBooks.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<List<BorrowedBooks>> GetAllAsync() 
        => await _context.BorrowedBooks.ToListAsync();

    public async Task<BorrowedBooks> GetByIdAsync(int id)
    {
        BorrowedBooks? borrowedBooks = await _context.BorrowedBooks.FirstOrDefaultAsync(b => b.Id == id);

        if (borrowedBooks == null)
            return new();

        return borrowedBooks;
    }

    public async Task<BorrowedBooks> UpdateAsync(int id, BorrowedBooks entity)
    {
        BorrowedBooks oldBorrowedBooks = await GetByIdAsync(id);

        if (oldBorrowedBooks.Id == 0)
            return oldBorrowedBooks;

        oldBorrowedBooks.DateBorrowed = entity.DateBorrowed;
        oldBorrowedBooks.ReturnDate = entity.ReturnDate;
        oldBorrowedBooks.User = entity.User;       

        _context.BorrowedBooks.Update(oldBorrowedBooks);
        await _context.SaveChangesAsync();
        return oldBorrowedBooks; 
    }
}