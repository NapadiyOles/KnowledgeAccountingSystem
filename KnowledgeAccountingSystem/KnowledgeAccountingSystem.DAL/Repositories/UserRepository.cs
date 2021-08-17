using System;
using KnowledgeAccountingSystem.DAL.Interfaces;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KnowledgeAccountingSystem.DAL.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeAccountingSystem.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly KnowledgeDbContext context;
        public UserRepository(KnowledgeDbContext _context)
        {
            context = _context;
        }
        public async Task AddAsync(User entity)
        {
            await context.Users.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            context.Users.Remove(await context.Users.FindAsync(id));
        }

        public IQueryable<User> FindAll()
        {
            return context.Users.AsNoTracking();
        }

        public async Task<User> GetByEmailPasswordAsync(string email, string password) => await Task.Run(() =>
         context.Users.FirstOrDefault(x => x.Email == email && x.Password == password));
        
        public async Task<User> GetByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public void Update(User entity)
        {
            context.Users.Update(entity);
        }
    }
}
