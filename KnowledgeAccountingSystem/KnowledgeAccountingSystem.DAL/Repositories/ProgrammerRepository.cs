using KnowledgeAccountingSystem.DAL.Entities;
using KnowledgeAccountingSystem.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task AddAsync(Programmer entity)
        {
            await context.Programmers.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            context.Programmers
                .Remove(await context.Programmers.FindAsync(id));
        }

        public IQueryable<Programmer> FindAll()
        {
            return context.Programmers
                .Include(x => x.Skills)
                .Include(x => x.User)
                .AsNoTracking();
        }

        public async Task<Programmer> GetByIdAsync(int id)
        {
            return await context.Programmers
                .Include(x => x.Skills)
                .Include(x => x.User)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Programmer entity)
        {
            context.Programmers.Update(entity);
        }
    }
}
