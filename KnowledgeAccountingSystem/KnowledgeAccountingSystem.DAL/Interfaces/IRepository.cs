using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeAccountingSystem.DAL.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        Task DeleteByIdAsync(int id);
        Task<TEntity> GetByIdAsync(int id);
        IQueryable<TEntity> FindAll();
    }
}
