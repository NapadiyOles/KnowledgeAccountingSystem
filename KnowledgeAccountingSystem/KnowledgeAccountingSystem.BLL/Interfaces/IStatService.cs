using KnowledgeAccountingSystem.BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeAccountingSystem.BLL.Interfaces
{
    public interface IStatService
    {
        IEnumerable<ManagerModel> GetTopManagers(int count);
        IEnumerable<DAL.Entities.skillArea> GetTheMostPopularSkills(int count);
        double GetAverageCountProgrammersByManager();
        IEnumerable<DAL.Entities.skillArea> GetTheLeastСommonSkills(int count);
        IEnumerable<DAL.Entities.skillArea> GetTheLeastPumpedSkills();
    }
}
