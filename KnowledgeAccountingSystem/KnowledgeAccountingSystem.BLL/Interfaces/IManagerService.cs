using KnowledgeAccountingSystem.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeAccountingSystem.BLL.Interfaces
{
    public interface IManagerService : IAccountService
    {
        IEnumerable<ProgrammerModel> GetAllProgrammers();
        IEnumerable<ProgrammerModelWithoutSkills> GetAllProgrammersWithoutSkills();
        Task SubscribeProgrammerAsync(int managerId, int programmerId);
        Task<IEnumerable<ProgrammerModel>> GetChoosenProgrammersByManagerIdAsync(int id);
        Task<ProgrammerModel> GetChoosenProgrammerByManagerIdAsync(int id, int programmerId);
        Task UnsubscribeProgrammerAsync(int id, int programmerId);
        IEnumerable<ProgrammerModel> GetProgrammersWithoutManagers();
    }
}
