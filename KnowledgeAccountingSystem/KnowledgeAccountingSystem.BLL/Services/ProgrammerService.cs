using AutoMapper;
using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Interfaces;
using KnowledgeAccountingSystem.BLL.Validation;
using KnowledgeAccountingSystem.BLL.ValidationExtensions;
using KnowledgeAccountingSystem.DAL.Entities;
using KnowledgeAccountingSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace KnowledgeAccountingSystem.BLL.Services
{
    public class ProgrammerService : IProgrammerService
    {
        private readonly IUnitOfWork context;
        private readonly IMapper mapper;
        public ProgrammerService(IUnitOfWork context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void AddSkill(int programmerId, SkillModel skill)
        {
            var programmer = context.ProgrammerRepository.GetByIdAsync(programmerId);
            if (programmer == null)
                throw new KnowledgeAccountException("Programmer not found", HttpStatusCode.NotFound);
            if (skill.IsSkillModelNotValid())
                throw new InvalidModelException("Uncorrect SkillModel", HttpStatusCode.BadRequest);
            if (skill.IsSkillExist(context, programmerId))
                throw new ResourceAlreadyExistException("This skill already exists");

            skill.ProgrammerId = programmerId;
            context.SkillRepository.Add(mapper.Map<Skill>(skill));
            context.Save();
        }

        public async Task DeleteAccountAsync(int id)
        {
            var user = (await context.ProgrammerRepository.GetByIdAsync(id)).User;
            await context.ProgrammerRepository.DeleteByIdAsync(id);
            await context.UserRepository.DeleteByIdAsync(user.Id);
            await context.SaveAsync();
        }

        public async Task DeleteSkillAsync(int programmerId, int skillId)
        {
            var programmer = await context.ProgrammerRepository?.GetByIdAsync(programmerId);
            if (programmer == null)
                throw new KnowledgeAccountException("Programmer not found", HttpStatusCode.NotFound);
            if (!programmer.Skills.Select(x => x.Id).Contains(skillId))
                throw new ResourceAlreadyExistException("Skill not found", HttpStatusCode.BadRequest);

            await context.SkillRepository.DeleteByIdAsync(skillId);
            await context.SaveAsync();
        }

        public async Task EditSkillAsync(int programmerId, SkillModel skill)
        {
            if (!skill.IsSkillExist(context, programmerId))
                throw new ResourceAlreadyExistException("Programmer don`t have this skill");
            if (skill.IsSkillModelNotValid())
                throw new InvalidModelException("Uncorrect SkillModel", HttpStatusCode.BadRequest);

            skill.ProgrammerId = programmerId;
            var sk = await context.SkillRepository.GetByIdAsync(skill.Id);
            context.SkillRepository.Update(mapper.Map(skill, sk));

            await context.SaveAsync();
        }

        public async Task<SkillModel> GetProgrammerSkillByIdAsync(int programmerId, int skillId)
        {
            var skills = await GetSkillsAsync(programmerId);
            SkillModel skill = skills.FirstOrDefault(x => x.Id == skillId);

            if (skill == null)
                throw new InvalidModelException("Skill not found", HttpStatusCode.BadRequest);

            return skill;
        }

        public int GetRoleId(int userId)
        {
            int? roleId = context.ProgrammerRepository.FindAll().FirstOrDefault(x => x.User.Id == userId)?.Id;
            if (!roleId.HasValue)
                throw new AuthorizeException("Unauthorized on this role", HttpStatusCode.Unauthorized);

            return roleId.Value;
        }

        public async Task<IEnumerable<SkillModel>> GetSkillsAsync(int programmerId)
        {
            return mapper.Map<IEnumerable<SkillModel>>((await Task.Run(() =>
                context.SkillRepository.GetAllProgrammersSkillsById(programmerId)))
                .AsEnumerable());
        }

        public async Task UpdateAccountAsync(UserModel model)
        {
            if (model.Id.IsAccountNotExist(context))
                throw new AuthorizeException("No programmers with same id!", HttpStatusCode.NotFound);
            if (model.IsModelInvalid())
                throw new InvalidModelException("Uncorrect user model", HttpStatusCode.BadRequest);

            var user = await context.UserRepository.GetByIdAsync(model.Id);
            context.UserRepository.Update(mapper.Map(model, user));
            await context.SaveAsync();
        }
    }

    public class CopyOfProgrammerService : IProgrammerService
    {
        private readonly IUnitOfWork context;
        private readonly IMapper mapper;
        public CopyOfProgrammerService(IUnitOfWork context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void AddSkill(int programmerId, SkillModel skill)
        {
            var programmer = context.ProgrammerRepository.GetByIdAsync(programmerId);
            if (programmer == null)
                throw new KnowledgeAccountException("Programmer not found", HttpStatusCode.NotFound);
            if (skill.IsSkillModelNotValid())
                throw new InvalidModelException("Uncorrect SkillModel", HttpStatusCode.BadRequest);
            if (skill.IsSkillExist(context, programmerId))
                throw new ResourceAlreadyExistException("This skill already exists");

            skill.ProgrammerId = programmerId;
            context.SkillRepository.Add(mapper.Map<Skill>(skill));
            context.Save();
        }

        public async Task DeleteAccountAsync(int id)
        {
            var user = (await context.ProgrammerRepository.GetByIdAsync(id)).User;
            await context.ProgrammerRepository.DeleteByIdAsync(id);
            await context.UserRepository.DeleteByIdAsync(user.Id);
            await context.SaveAsync();
        }

        public async Task DeleteSkillAsync(int programmerId, int skillId)
        {
            var programmer = await context.ProgrammerRepository?.GetByIdAsync(programmerId);
            if (programmer == null)
                throw new KnowledgeAccountException("Programmer not found", HttpStatusCode.NotFound);
            if (!programmer.Skills.Select(x => x.Id).Contains(skillId))
                throw new ResourceAlreadyExistException("Skill not found", HttpStatusCode.BadRequest);

            await context.SkillRepository.DeleteByIdAsync(skillId);
            await context.SaveAsync();
        }

        public async Task EditSkillAsync(int programmerId, SkillModel skill)
        {
            if (!skill.IsSkillExist(context, programmerId))
                throw new ResourceAlreadyExistException("Programmer don`t have this skill");
            if (skill.IsSkillModelNotValid())
                throw new InvalidModelException("Uncorrect SkillModel", HttpStatusCode.BadRequest);

            skill.ProgrammerId = programmerId;
            var sk = await context.SkillRepository.GetByIdAsync(skill.Id);
            context.SkillRepository.Update(mapper.Map(skill, sk));

            await context.SaveAsync();
        }

        public async Task<SkillModel> GetProgrammerSkillByIdAsync(int programmerId, int skillId)
        {
            var skills = await GetSkillsAsync(programmerId);
            SkillModel skill = skills.FirstOrDefault(x => x.Id == skillId);

            if (skill == null)
                throw new InvalidModelException("Skill not found", HttpStatusCode.BadRequest);

            return skill;
        }

        public int GetRoleId(int userId)
        {
            int? roleId = context.ProgrammerRepository.FindAll().FirstOrDefault(x => x.User.Id == userId)?.Id;
            if (!roleId.HasValue)
                throw new AuthorizeException("Unauthorized on this role", HttpStatusCode.Unauthorized);

            return roleId.Value;
        }

        public async Task<IEnumerable<SkillModel>> GetSkillsAsync(int programmerId)
        {
            return mapper.Map<IEnumerable<SkillModel>>((await Task.Run(() =>
                context.SkillRepository.GetAllProgrammersSkillsById(programmerId)))
                .AsEnumerable());
        }

        public async Task UpdateAccountAsync(UserModel model)
        {
            if (model.Id.IsAccountNotExist(context))
                throw new AuthorizeException("No programmers with same id!", HttpStatusCode.NotFound);
            if (model.IsModelInvalid())
                throw new InvalidModelException("Uncorrect user model", HttpStatusCode.BadRequest);

            var user = await context.UserRepository.GetByIdAsync(model.Id);
            context.UserRepository.Update(mapper.Map(model, user));
            await context.SaveAsync();
        }
    }
}
