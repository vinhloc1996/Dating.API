using System.Collections.Generic;
using System.Threading.Tasks;
using Dating.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Dating.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;
        public DatingRepository(DataContext context) => _context = context;
        public void Add<T>(T entity) where T : class => _context.Add(entity);

        public void Delete<T>(T entity) where T : class => _context.Remove(entity);

        public async Task<User> GetUser(int id) => await _context.User.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);

        public async Task<IEnumerable<User>> GetUsers() => await _context.User.Include(p => p.Photos).ToListAsync();

        public async Task<bool> SaveAll() => await _context.SaveChangesAsync() > 0;
    }
}