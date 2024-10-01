using Model;
using Microsoft.EntityFrameworkCore;

namespace Context;
public class PostgresContext : DbContext
{
    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
    {
    }
    
    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<BorrowedBooks> BorrowedBooks { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().ToTable("book");
        modelBuilder.Entity<User>().ToTable("user");
        modelBuilder.Entity<BorrowedBooks>().ToTable("borrowedbooks")
            .HasMany(b => b.Books);
        modelBuilder.Entity<BorrowedBooks>().HasOne(b => b.User);
    }
}