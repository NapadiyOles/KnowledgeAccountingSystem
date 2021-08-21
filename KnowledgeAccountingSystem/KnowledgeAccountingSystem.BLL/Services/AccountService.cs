using AutoMapper;
using AutoMapper.Configuration;
using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Interfaces;
using KnowledgeAccountingSystem.BLL.Validation;
using KnowledgeAccountingSystem.BLL.ValidationExtensions;
using KnowledgeAccountingSystem.DAL.Entities;
using KnowledgeAccountingSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeAccountingSystem.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork context;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        public AccountService(IUnitOfWork context, IMapper mapper, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.context = context;
            this.mapper = mapper;
        }
      
        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await FindUserAsync(email,password);
            if (user == null)
                throw new AuthorizeException("Unauthorized", HttpStatusCode.Unauthorized);
            else
                return GetToken(user);
        }

        public async Task RegistrationAsync(UserModel model)
        {
            if (model.IsModelInvalid())
                throw new InvalidModelException("uncorrect user model", HttpStatusCode.BadRequest);
            if (model.Email.IsEmailAlreadyExist(context))
                throw new ResourceAlreadyExistException("Email already exist", HttpStatusCode.BadRequest);

            var user = mapper.Map<User>(model);
            user.Role = Roles.Programmer;
            await context.ProgrammerRepository.AddAsync(new Programmer { User = user });
            await context.SaveAsync();
        }

        private async Task<UserModel> FindUserAsync(string email, string password)
        {
            return mapper.Map<UserModel>(await context.UserRepository.GetByEmailPasswordAsync(email, password));
        }

        private string GetToken(UserModel user)
        {
            return "111";
        }
    }
}
