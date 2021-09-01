﻿using AutoMapper;
using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Interfaces;
using KnowledgeAccountingSystem.BLL.Validation;
using KnowledgeAccountingSystem.DAL.Entities;
using KnowledgeAccountingSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<skillArea> GetTheLeastPumpedSkills()
        {
            try
            {
                List<skillArea> skillNames = new List<skillArea>();
                var skills = context.SkillRepository.FindAll().Select(x => new { x.Name, x.Lvl }).Where(x => x.Lvl == lvl.Advanced);

                var result = skills.AsEnumerable()
                    .GroupBy(x => x.Name)
                    .ToDictionary(x => x.Key, x => x.ToDictionary(y => y.Lvl, y => x.Count()));
                foreach (var skill in result)
                {
                    int low = skill.Value[lvl.Low];
                    int middle = skill.Value[lvl.Middle];
                    int hight = skill.Value[lvl.Advanced];
                    if (NeedMoreLections(low, middle, hight))
                        skillNames.Add(skill.Key);
                }
                return skillNames.AsEnumerable();  
            }
            catch
            {
                throw new KnowledgeAccountException("Something went wrong");
            }
        }

        private bool NeedMoreLections(int low, int middle, int hight)
        {
            var sum = low + middle + hight;
            var rate = (low / sum) * 100;
            if (rate > 40)
                return true;
            return false;
        }
        public IEnumerable<skillArea> GetTheLeastСommonSkills(int count)
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
                    .OrderBy(x => x.Count())
                    .Select(x => x.Key)
                    .Take(count);

                return result;
            }
            catch (KnowledgeAccountException)
            {
                throw new KnowledgeAccountException("Something went wrong");
            }
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
                throw new KnowledgeAccountException("Something went wrong");
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
                throw new KnowledgeAccountException("Something went wrong");
            }
        }
    }
}
