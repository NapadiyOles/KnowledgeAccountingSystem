using KnowledgeAccountingSystem.BLL.DTO;
using System.Collections.Generic;

namespace KnowledgeAccountingSystem.BLL.Interfaces
{
    public interface IStatService
    {
        IEnumerable<ManagerModel> GetTopManagers(int count);
        IEnumerable<DAL.Entities.skillArea> GetTheMostPopularSkills(int count);
    }
}
