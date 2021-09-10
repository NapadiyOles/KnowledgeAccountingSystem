using AutoMapper;
using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Interfaces;
using KnowledgeAccountingSystem.BLL.Validation;
using KnowledgeAccountingSystem.DAL.Entities;
using KnowledgeAccountingSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

        public double GetAverageCountProgrammersByManager()
        {
            try
            {
                var average = (context.ManagerRepository.FindAll())
                    .Select(x => x.Programmers.Count())
                    .AsEnumerable()
                    .Average();
                return average;
            }
            catch
            {
                throw new KnowledgeAccountException("Something went wrong");
            }
        }

        public IEnumerable<string> GetTheLeastPumpedSkills()
        {
                var skills = context.SkillRepository.FindAll().Select(x => new { x.Name, x.Lvl });

                var result = skills.AsEnumerable()
                    .GroupBy(x => x.Name)
                    .ToDictionary(x => x.Key, x => x.GroupBy(x => x.Lvl).ToDictionary(y => y.Key, y => x.Count()));
                foreach (var skill in result)
                {
                    int low = 0;
                    int middle = 0;
                    int hight = 0;
                    if (skill.Value.ContainsKey(lvl.Low))
                        low = skill.Value[lvl.Low];
                    if (skill.Value.ContainsKey(lvl.Middle))
                        middle = skill.Value[lvl.Middle];
                    if (skill.Value.ContainsKey(lvl.Advanced))
                        hight = skill.Value[lvl.Advanced];
                    if (NeedMoreLections(low, middle, hight))
                             yield return skill.Key.ToString("g");
                }         
        }

        private bool NeedMoreLections(int low, int middle, int hight)
        {
            var sum = low + middle + hight;
            var rate = (low / sum) * 100;
            if (rate > 35)
                return true;
            return false;
        }
        public IEnumerable<string> GetTheLeastСommonSkills(int count)
        {
           
                var programmers = context.ProgrammerRepository.FindAll();

                var skills = programmers
                    .Select(x => x.Skills
                    .Select(y => y.Name));

                var result = skills.AsEnumerable()
                    .Aggregate((current, next) => current.Concat(next))
                    .GroupBy(x => x)
                    .OrderBy(x => x.Count())
                    .Select(x => x.Key)
                    .Take(count);

                foreach (var item in result)
                {
                    yield return item.ToString("g");
                }          
        }

        public IEnumerable<string> GetTheMostPopularSkills(int count)
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
                foreach (var item in result)
                {
                    yield return item.ToString("g");
                }
        }

        public IEnumerable<ManagerModelWithoutProgrammers> GetTopManagers(int count)
        {
            try
            {
                return mapper
                    .Map<IEnumerable<ManagerModelWithoutProgrammers>>(context.ManagerRepository
                    .FindAll()
                    .OrderByDescending(x => x.Programmers.Count())
                    .Take(count).AsEnumerable());
            }
            catch (KnowledgeAccountException)
            {
                throw new KnowledgeAccountException("Something went wrong");
            }
        }

        public IEnumerable<string> GetTheLeastPumpedSkillsByManagerId(int id)
        {
            var manager = context.ManagerRepository.GetByIdAsync(id).Result;
            if (manager == null)
                throw new ArgumentException("Manager with this id not found");
            var programmersId = context.ProgrammerRepository
                .FindAll()
                .Where(x => x.ManagerId == id)
                .Select(y => y.Id);
            var skills = context.SkillRepository
                .FindAll()
                .Where(x => programmersId.Contains(x.ProgrammerId))
                .Select(x => new { x.Name, x.Lvl });

                var result = skills.AsEnumerable()
                    .GroupBy(x => x.Name)
                    .ToDictionary(x => x.Key, x => x.GroupBy(x => x.Lvl).ToDictionary(y => y.Key, y => x.Count()));
                foreach (var skill in result)
                {
                    int low = 0;
                    int middle = 0;
                    int hight = 0;
                    if (skill.Value.ContainsKey(lvl.Low))
                        low = skill.Value[lvl.Low];
                    if(skill.Value.ContainsKey(lvl.Middle))
                        middle = skill.Value[lvl.Middle];
                    if (skill.Value.ContainsKey(lvl.Advanced))
                        hight = skill.Value[lvl.Advanced];
                    if (NeedMoreLections(low, middle, hight))
                         yield return skill.Key.ToString("g");
                }
        }
    }
}
