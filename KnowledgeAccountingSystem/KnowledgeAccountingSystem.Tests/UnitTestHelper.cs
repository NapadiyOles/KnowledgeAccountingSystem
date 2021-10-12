using System;
using AutoMapper;
using KnowledgeAccountingSystem.DAL;
using KnowledgeAccountingSystem.BLL;
using KnowledgeAccountingSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using KnowledgeAccountingSystem.BLL.Mapper;
using System.Text;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace KnowledgeAccountingSystem.Tests
{
    internal static class UnitTestHelper
    {
        public static DbContextOptions<KnowledgeDbContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<KnowledgeDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new KnowledgeDbContext(options))
            {
                SeedData(context);
            }
            return options;
        }

        public static void SeedData(KnowledgeDbContext context)
        {
            var manager = new User
            {
                Id = 1,
                Name = "Jon",
                Surname = "Snow",
                Email = "Jon@gmail.com",
                Password = Encrypt("qwerty"),
                Role = Roles.Manager
            };

            context.Users.Add(manager);

            var user1 = new User
            {
                Id = 2,
                Name = "Maria",
                Surname = "Soroka",
                Email = "Maria@gmail.com",
                Password = Encrypt("qwerty"),
                Role = Roles.Programmer
            };
            context.Users.Add(user1);

            var user2 = new User
            {
                Id = 3,
                Name = "Kostiantyn",
                Surname = "Salabai",
                Email = "Salabai@gmail.com",
                Password = Encrypt("qwerty"),
                Role = Roles.Programmer
            };
            context.Users.Add(user2);

            context.SaveChanges();

            context.Managers.Add(new Manager
            {
                Id = 1,
                User = manager
            });

            context.SaveChanges();

            context.Programmers.Add(new Programmer
            {
                Id = 1,
                ManagerId = 1,
                User = user1
            });

            context.Programmers.Add(new Programmer
            {
                Id = 2,
                ManagerId = 1,
                User = user2
            });
          
            context.SaveChanges();

            context.Skills.Add( new Skill
            {
                Id = 1,
                Name = skillArea.DotNet,
                Lvl = lvl.Advanced,
                ProgrammerId = 1
            });
           context.Skills.Add(new Skill
            {
                Id = 2,
                Name =skillArea.JavaScript,
                Lvl = lvl.Low,
                ProgrammerId = 2
            });

            context.SaveChanges();
        }
        public static string Encrypt(string password)
        {
            var data = Encoding.Unicode.GetBytes(password);
            var encrypted = ProtectedData.Protect(data, null, DataProtectionScope.LocalMachine);
            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(string password)
        {
            var data = Convert.FromBase64String(password);
            var decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.LocalMachine);
            return Encoding.Unicode.GetString(decrypted);
        }

        public static string GetProgrammersWithId1Token()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            string Secret = "h24s71l32dinab56";
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, 2.ToString()),
                    new Claim(ClaimTypes.Role, "Programmer")
                }),
                Expires = DateTime.UtcNow.AddSeconds(10000),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string GetManagersWithId1Token()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            string Secret = "h24s71l32dinab56";
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, 1.ToString()),
                    new Claim(ClaimTypes.Role, "Manager")
                }),
                Expires = DateTime.UtcNow.AddSeconds(10000),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static Mapper CreateMapperProfile()
        {
            var myProfile = new AutomapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

            return new Mapper(configuration);
        }
    }
}
