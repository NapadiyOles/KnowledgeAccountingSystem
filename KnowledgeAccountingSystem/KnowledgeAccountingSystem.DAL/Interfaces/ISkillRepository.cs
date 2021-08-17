using KnowledgeAccountingSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnowledgeAccountingSystem.DAL.Interfaces
{
    public interface ISkillRepository : IRepository<Skill>
    {
        IQueryable<Skill> GetAllProgrammersSkillsById(int programmerId);
    }
}
