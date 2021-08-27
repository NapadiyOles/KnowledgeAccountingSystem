using KnowledgeAccountingSystem.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeAccountingSystem.BLL.Interfaces
{
    public interface IStatService
    {
        IEnumerable<ManagerModel> GetTopManagers(int count);
        IEnumerable<DAL.Entities.skillArea> GetTheMostPopularSkills(int count);

    }
}
