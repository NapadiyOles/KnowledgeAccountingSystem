using AutoMapper;
using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Interfaces;
using KnowledgeAccountingSystem.BLL.Validation;
using KnowledgeAccountingSystem.DAL.Entities;
using KnowledgeAccountingSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnowledgeAccountingSystem.BLL.Services
{
    public class StatisticService : IStatService
    {
        private readonly IUnitOfWork context;
        private readonly IMapper mapper;

        public StatisticService(IUnitOfWork context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IEnumerable<skillArea> GetTheMostPopularSkills(int count)
        {
            try
            {
                var programmers = context.ProgrammerRepository.FindAll();

                var skills = programmers
                    .Select(x => x.Skills
                    .Select(y => y.Name));
                
                var result = skills.AsEnumerable()
                    .Aggregate((current, next) => current.Concat(next))
                    .GroupBy(x => x)
                    .OrderByDescending(x => x.Count())
                    .Select(x => x.Key)
                    .Take(count);

                return result;
            }
            catch (KnowledgeAccountException)
            {
                throw new KnowledgeAccountException();
            }
        }

        public IEnumerable<ManagerModel> GetTopManagers(int count)
        {
            try
            {
                return mapper
                    .Map<IEnumerable<ManagerModel>>(context.ManagerRepository
                    .FindAll()
                    .OrderByDescending(x => x.Programmers.Count())
                    .Take(count).AsEnumerable());
            }
            catch (KnowledgeAccountException)
            {
                throw new KnowledgeAccountException();
            }
        }
    }
}
