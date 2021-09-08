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
    public class SkillRepository : ISkillRepository
    {
        readonly KnowledgeDbContext context;
        public SkillRepository(KnowledgeDbContext _context)
        {
            context = _context;
        }

        public void Add(Skill entity)
        {
            context.Skills.Add(entity);
        }

        public async Task AddAsync(Skill entity)
        {
            await context.Skills.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            context.Skills.Remove(await context.Skills.FindAsync(id));
        }

        public IQueryable<Skill> FindAll()
        {
            return context.Skills.Include(x => x.Programmer).AsNoTracking();
        }

        public IQueryable<Skill> GetAllProgrammersSkillsById(int programmerId)
        {
            return context.Skills.Where(x => x.Programmer.Id == programmerId);
        }

        public async Task<Skill> GetByIdAsync(int id)
        {
            return await context.Skills.Include(x => x.Programmer)
            .SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Skill entity)
        {
            context.Skills.Update(entity);
        }
    }
}
