using KnowledgeAccountingSystem.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeAccountingSystem.BLL.Interfaces
{
    public interface IProgrammerService : IAccountService
    {
        Task<IEnumerable<SkillModel>> GetSkillsAsync(int programmerId);
        Task<SkillModel> GetProgrammerSkillByIdAsync(int programmerId, int skillId);
        Task AddSkillAsync(int programmerId, SkillModel skill);
        Task DeleteSkillAsync(int programmerId, int skillId);
        Task EditSkillAsync(int programmerId, SkillModel skill);
    }
}
