using System;
using KnowledgeAccountingSystem.DAL.Interfaces;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KnowledgeAccountingSystem.DAL.Entities;
using System.Linq;

namespace KnowledgeAccountingSystem.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByEmailPasswordAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
