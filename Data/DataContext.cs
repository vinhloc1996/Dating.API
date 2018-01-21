using Dating.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Dating.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Value> Value { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Photo> Photo { get; set; }
    }
}