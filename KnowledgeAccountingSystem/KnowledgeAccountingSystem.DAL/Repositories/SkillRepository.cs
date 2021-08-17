using System;
using KnowledgeAccountingSystem.DAL.Interfaces;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KnowledgeAccountingSystem.DAL.Entities;
using System.Linq;

namespace KnowledgeAccountingSystem.DAL.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        public Task AddAsync(Skill entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Skill> FindAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Skill> GetAllProgrammersSkillsById(int programmerId)
        {
            throw new NotImplementedException();
        }

        public Task<Skill> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Skill entity)
        {
            throw new NotImplementedException();
        }
    }
}
