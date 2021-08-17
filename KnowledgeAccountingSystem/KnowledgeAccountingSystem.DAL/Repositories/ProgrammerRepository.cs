using KnowledgeAccountingSystem.DAL.Entities;
using KnowledgeAccountingSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeAccountingSystem.DAL.Repositories
{
    public class ProgrammerRepository : IProgrammerRepository
    {
        readonly KnowledgeDbContext context;
        public ProgrammerRepository(KnowledgeDbContext _context)
        {
            context = _context;
        }

        public Task AddAsync(Programmer entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Programmer> FindAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Programmer> FindAllWithDetails()
        {
            throw new NotImplementedException();
        }

        public Task<Programmer> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Programmer> GetByIdWithDetailsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Programmer entity)
        {
            throw new NotImplementedException();
        }
    }
}
