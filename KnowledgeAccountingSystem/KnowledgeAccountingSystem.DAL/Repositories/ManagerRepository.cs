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
    public class ManagerRepository : IManagerRepository
    {
        readonly KnowledgeDbContext context;
        public ManagerRepository(KnowledgeDbContext _context)
        {
            context = _context;
        }

        public async Task AddAsync(Manager entity)
        {
            await context.Managers.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            context.Managers.Remove(await context.Managers.FindAsync(id));
        }

        public IQueryable<Manager> FindAll()
        {
            return context.Managers
            .Include(x => x.Programmers)
            .Include(x => x.User)
            .AsNoTracking();
        }

        public async Task<IEnumerable<Programmer>> GetAllChoosenProgrammersAsync(int id) => (await GetByIdAsync(id)).Programmers
            .Select(x => GetProgrammerByIdAsync(x.Id).Result);

        public async Task<Manager> GetByIdAsync(int id)
        {
            return await context.Managers
            .Include(x => x.Programmers)
            .Include(x => x.User)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task SelectProgrammerAsync(int managerId, int programmerId)
        {
            var programmer = await GetProgrammerByIdAsync(programmerId);
            programmer.ManagerId = managerId;
            context.Programmers.Update(programmer);
        }

        private async Task<Programmer> GetProgrammerByIdAsync(int programmerId) => await context.Programmers
            .Include(x => x.Skills)
            .Include(x => x.User)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == programmerId);

        public async Task UnsubscribeProgrammerAsync(int managerId, int programmerId)
        {
            var programmer = await GetProgrammerByIdAsync(programmerId);
            programmer.ManagerId = default;
            context.Programmers.Update(programmer);
        }

        public void Update(Manager entity)
        {
            context.Managers.Update(entity);
        }
    }
}
