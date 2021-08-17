using System;
using KnowledgeAccountingSystem.DAL.Interfaces;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KnowledgeAccountingSystem.DAL.Entities;
using System.Linq;

namespace KnowledgeAccountingSystem.DAL.Repositories
{
    public class ManagerRepository : IManagerRepository
    {
        public Task AddAsync(Manager entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Manager> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Programmer>> GetAllChoosenProgrammersAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Manager> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SelectProgrammerAsync(int managerId, int programmerId)
        {
            throw new NotImplementedException();
        }

        public Task UnsubscribeProgrammerAsync(int managerId, int programmerId)
        {
            throw new NotImplementedException();
        }

        public void Update(Manager entity)
        {
            throw new NotImplementedException();
        }
    }
}
