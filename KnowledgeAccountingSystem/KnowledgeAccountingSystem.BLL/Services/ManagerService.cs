using AutoMapper;
using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Interfaces;
using KnowledgeAccountingSystem.BLL.Validation;
using KnowledgeAccountingSystem.BLL.ValidationExtensions;
using KnowledgeAccountingSystem.DAL.Entities;
using KnowledgeAccountingSystem.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace KnowledgeAccountingSystem.BLL.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IUnitOfWork context;
        private readonly IMapper mapper;
        public ManagerService(IUnitOfWork context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task DeleteAccountAsync(int id)
        {
            var user = (await context.ManagerRepository.GetByIdAsync(id)).User;
            await context.ManagerRepository.DeleteByIdAsync(id);
            await context.UserRepository.DeleteByIdAsync(user.Id);
            await context.SaveAsync();
        }

        public async Task UnsubscribeProgrammerAsync(int id, int programmerId)
        {
            IEnumerable<ProgrammerModel> choosenProgrammers = await GetChoosenProgrammersByManagerIdAsync(id);
            ProgrammerModel programmer = choosenProgrammers.FirstOrDefault(x => x.Id == programmerId);

            if (programmer.Equals(null))
                throw new KnowledgeAccountException("Manager didnt choose this programmer");

            await context.ManagerRepository.UnsubscribeProgrammerAsync(id, programmerId);
            await context.SaveAsync();
        }

        public IEnumerable<ProgrammerModel> GetAllProgrammers()
        {
           return mapper.Map<IEnumerable<ProgrammerModel>>(context.ProgrammerRepository.FindAll().AsEnumerable());
        }

        public async Task<ProgrammerModel> GetChoosenProgrammerByManagerIdAsync(int id, int programmerId)
        {
            IEnumerable<ProgrammerModel> choosen = await GetChoosenProgrammersByManagerIdAsync(id);
            ProgrammerModel programmer = choosen.FirstOrDefault(x => x.Id == programmerId);

            if (programmer == null)
                throw new KnowledgeAccountException("Manager didnt choose this programmer");

            return mapper.Map<ProgrammerModel>(programmer);
        }

        public async Task<IEnumerable<ProgrammerModel>> GetChoosenProgrammersByManagerIdAsync(int id)
        {
            IEnumerable<ProgrammerModel> choosenProgrammers = mapper.Map<IEnumerable<ProgrammerModel>>
                (await context.ManagerRepository?.GetAllChoosenProgrammersAsync(id));
            return choosenProgrammers;
        }

        public int GetRoleId(int userId)
        {
            int? roleId = context.ManagerRepository.FindAll().FirstOrDefault(x => x.User.Id == userId)?.Id;
            if (!roleId.HasValue)
                throw new AuthorizeException("Unauthorize on this role", HttpStatusCode.BadRequest);
            return roleId.Value;
        }

        public async Task SubscribeProgrammerAsync(int managerId, int programmerId)
        {
            Programmer programmer = await context.ProgrammerRepository?.GetByIdAsync(programmerId);
            if (programmer == null)
                throw new InvalidModelException("Uncorrect choosen programmer id");
            if (programmer.ManagerId != null)
                throw new ResourceAlreadyExistException("Programmer already has manager");

            await context.ManagerRepository.SelectProgrammerAsync(managerId, programmerId);
            await context.SaveAsync();
        }

        public async Task UpdateAccountAsync(UserModel model)
        {
            if (model.Id.IsAccountNotExist(context))
                throw new AuthorizeException("No found programmers with the same id!", HttpStatusCode.BadRequest);
            if (model.IsModelInvalid())
                throw new InvalidModelException("Uncorrect user model", HttpStatusCode.BadRequest);

            var user = await context.UserRepository.GetByIdAsync(model.Id);
            context.UserRepository.Update(mapper.Map(model, user));
            await context.SaveAsync();
        }

        public IEnumerable<ProgrammerModel> GetProgrammersWithoutManagers()
        {
            var programmers = context.ProgrammerRepository.FindAll().AsEnumerable();
            var programmersWithoutManager = programmers.Where(x => x.Manager == null);
            return mapper.Map<IEnumerable<ProgrammerModel>>(programmersWithoutManager);
        }
    }
}
