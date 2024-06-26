using Microsoft.EntityFrameworkCore;

namespace post_service;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Post> Posts { get; set; }
}