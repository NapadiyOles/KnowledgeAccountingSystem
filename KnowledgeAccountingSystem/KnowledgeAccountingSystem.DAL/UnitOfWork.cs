using KnowledgeAccountingSystem.DAL.Interfaces;
using KnowledgeAccountingSystem.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeAccountingSystem.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly KnowledgeDbContext context;

        public UnitOfWork(KnowledgeDbContext _context)
        {
            context = _context;
        }

        ManagerRepository managerRepository { get; set; }
        ProgrammerRepository programmerRepository { get; set; }
        SkillRepository skillRepository { get; set; }
        UserRepository userRepository { get; set; }

        public IManagerRepository ManagerRepository
        {
            get
            {
                if (managerRepository == null)
                {
                    managerRepository = new ManagerRepository(context);
                }
                return managerRepository;
            }
        }

        public IProgrammerRepository ProgrammerRepository
        {
            get
            {
                if (programmerRepository == null)
                {
                    programmerRepository = new ProgrammerRepository(context);
                }
                return programmerRepository;
            }
        }

        public ISkillRepository SkillRepository
        {
            get
            {
                if (skillRepository == null)
                {
                    skillRepository = new SkillRepository(context);
                }
                return skillRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(context);
                }
                return userRepository;
            }
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
