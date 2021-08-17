using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeAccountingSystem.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IManagerRepository ManagerRepository { get; }

        IProgrammerRepository ProgrammerRepository { get; }

        ISkillRepository SkillRepository { get; }

        IUserRepository UserRepository { get; }

        Task SaveAsync();
        void Save();
    }
}
