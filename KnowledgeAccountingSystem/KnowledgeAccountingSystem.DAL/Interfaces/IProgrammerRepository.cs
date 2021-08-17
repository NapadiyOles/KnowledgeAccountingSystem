using KnowledgeAccountingSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeAccountingSystem.DAL.Interfaces
{
    public interface IProgrammerRepository : IRepository<Programmer>
    {
        IQueryable<Programmer> FindAllWithDetails();
        Task<Programmer> GetByIdWithDetailsAsync(int id);
    }
}
