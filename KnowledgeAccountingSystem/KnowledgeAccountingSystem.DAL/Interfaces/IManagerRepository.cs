using KnowledgeAccountingSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeAccountingSystem.DAL.Interfaces
{
    public interface IManagerRepository : IRepository<Manager>
    {
        Task SelectProgrammerAsync(int managerId, int programmerId);
        Task<IEnumerable<Programmer>> GetAllChoosenProgrammersAsync(int id);
        Task UnsubscribeProgrammerAsync(int managerId, int programmerId);
    }
}
