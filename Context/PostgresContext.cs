using Model;
using Microsoft.EntityFrameworkCore;

namespace Context;
public class PostgresContext : DbContext
{
    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
    {
    }
    
    public DbSet<Book> Books { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().ToTable("book");
    }
}