using AutoMapper;
using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Interfaces;
using KnowledgeAccountingSystem.BLL.Validation;
using KnowledgeAccountingSystem.BLL.ValidationExtensions;
using KnowledgeAccountingSystem.DAL.Entities;
using KnowledgeAccountingSystem.DAL.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using KnowledgeAccountingSystem.BLL.JWT;

namespace KnowledgeAccountingSystem.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork context;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IOptions<AuthOptions> authOptions;
        public AuthService(IUnitOfWork context, IMapper mapper, IConfiguration configuration, IOptions<AuthOptions> authOptions)
        {
            this.configuration = configuration;
            this.context = context;
            this.mapper = mapper;
            this.authOptions = authOptions;
            AddDefaultAdmin().Wait();
        }

        private string Encrypt(string password)
        {
            var data = Encoding.Unicode.GetBytes(password);
            var encrypted = ProtectedData.Protect(data, null, DataProtectionScope.LocalMachine);
            return Convert.ToBase64String(encrypted);
        }

        private string Decrypt(string password)
        {
            var data = Convert.FromBase64String(password);
            var decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.LocalMachine);
            return Encoding.Unicode.GetString(decrypted);
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await FindUserAsync(email);
            if (user == null)
                throw new AuthorizeException("Unauthorized", HttpStatusCode.Unauthorized);
            if (password != Decrypt(user.Password))
                throw new AuthorizeException("Password is not correct", HttpStatusCode.Unauthorized);
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
            user.Password = Encrypt(model.Password);
            await context.ProgrammerRepository.AddAsync(new Programmer { Id =user.Id, User = user });
            await context.SaveAsync();
        }

        private async Task<UserModel> FindUserAsync(string email)
        {
            return mapper.Map<UserModel>(await context.UserRepository.GetByEmailAsync(email));
        }

        private string GetToken(UserModel user)
        {
            var authParams = authOptions.Value;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = authParams.GetSymmetricSecurityKey();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddSeconds(authParams.TokenLifeTime),
                SigningCredentials = new SigningCredentials( key, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task AddDefaultAdmin()
        {
            if (await context.UserRepository.GetOneAsync(u => u.Role == Roles.Administrator) is null)
            {
                await context.UserRepository.AddAsync(new User
                { Name = "admin", Surname = "admin", Email="KnowledgeAccountingAdmin@gmail.com", Password = Encrypt("admin"), Role = Roles.Administrator });
                await context.SaveAsync();
            }
        }
    }
}
