using Context;
using DTO;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Services;

public class UserService : ICommonService<User>
{
    private readonly PostgresContext _context;
    public UserService(PostgresContext context)
    {
        _context = context;
    }

    public async Task<User> CreateAsync(User entity)
    {
        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<User> DeleteAsync(User entity)
    {
        _context.Users.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<List<User>> GetAllAsync() 
        => await _context.Users.ToListAsync();

    public async Task<User> GetByIdAsync(int id)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return new();

        return user;
    }

    public async Task<User> UpdateAsync(int id, User entity)
    {
        User oldUser = await GetByIdAsync(id);

        if (oldUser.Id == 0)
            return oldUser;

        oldUser.Email = entity.Email;
        oldUser.Name = entity.Name;
        oldUser.Password = entity.Password;
        oldUser.IsActived = entity.IsActived;

        _context.Users.Update(oldUser);
        await _context.SaveChangesAsync();
        return entity;
    }
}
