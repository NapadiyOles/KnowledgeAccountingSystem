using KnowledgeAccountingSystem.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeAccountingSystem.BLL.Interfaces
{
    public interface IStatService
    {
        IEnumerable<ManagerModelWithoutProgrammers> GetTopManagers(int count);
        IEnumerable<string> GetTheMostPopularSkills(int count);
        double GetAverageCountProgrammersByManager();
        IEnumerable<string> GetTheLeastСommonSkills(int count);
        IEnumerable<string> GetTheLeastPumpedSkills();
        IEnumerable<string> GetTheLeastPumpedSkillsByManagerId(int id);

    }
}
