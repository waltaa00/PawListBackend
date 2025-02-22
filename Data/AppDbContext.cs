using Microsoft.EntityFrameworkCore;
using PawListBackend.Models;

namespace PawListBackend.Data
{
    public class AppDbContext : DbContext
    {
        // DbSet for Dog entities.
        public DbSet<Dog> Dogs { get; set; }

        // DbSet for User entities.
        public DbSet<User> Users { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
    }
}