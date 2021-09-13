using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Services;
using KnowledgeAccountingSystem.DAL.Entities;
using KnowledgeAccountingSystem.DAL.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace KnowledgeAccountingSystem.Tests.BusinessTests
{
    public class ManagerServiceTests
    {
        [Test]
        public void ManagerService_GetAllProgrammers_ReturnsProgrammerModels()
        {
            //Arrange
            var comparer = new ProgrammerModelEqualityComparer();
            var expected = GetTestProgrammerModels().ToList();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.ProgrammerRepository.FindAll())
                .Returns(GetTestProgrammerEntities().AsQueryable);
            var managerService = new ManagerService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //Act
            var actual = managerService.GetAllProgrammers().ToList();

            // Assert
            for (int i = 0; i < actual.Count; i++)
            {
                mockUnitOfWork
                    .Verify(uow => uow.ProgrammerRepository.FindAll(), 
                    Times.Once());
                Assert.IsTrue(comparer.Equals(expected[i], actual[i]));
                for (int j = 0; j < actual[i].Skills.Count(); j++)
                {
                    Assert.AreEqual(expected[i].Skills.ElementAt(j).Name, actual[i].Skills.ElementAt(j).Name);
                    Assert.AreEqual(expected[i].Skills.ElementAt(j).Lvl, actual[i].Skills.ElementAt(j).Lvl);
                }
            }
        }

        private IEnumerable<ProgrammerModel> GetTestProgrammerModels()
        {
            var skill1 = new SkillModel
            {
                Id = 1,
                Name = "DotNet",
                Lvl = "Advanced",
                ProgrammerId = 1
            };
            var skill2 = new SkillModel
            {
                Id = 2,
                Name = "JavaScript",
                Lvl = "Low",
                ProgrammerId = 2
            };

            return new List<ProgrammerModel>()
            {
                new ProgrammerModel {
                    Id = 1,
                    Name = "Maria",
                    Surname = "Soroka",
                    Skills = new List<SkillModel>() { skill1 }
                },
                new ProgrammerModel {
                    Id = 2,
                    Name = "Kostiantyn",
                    Surname = "Salabai",
                    Skills = new List<SkillModel>() { skill2 }
                }
            };
        }
        private List<Programmer> GetTestProgrammerEntities()
        {
            var skill1 = new Skill
            {
                Id = 1,
                Name = skillArea.DotNet,
                Lvl = lvl.Advanced,
                ProgrammerId = 1
            };
            var skill2 = new Skill
            {
                Id = 2,
                Name = skillArea.JavaScript,
                Lvl = lvl.Low,
                ProgrammerId = 2
            };

            var user1 = new User
            {
                Id = 2,
                Name = "Maria",
                Surname = "Soroka",
                Email = "Maria@gmail.com",
                Password = UnitTestHelper.Encrypt("qwerty"),
                Role = Roles.Programmer
            };

            var user2 = new User
            {
                Id = 3,
                Name = "Kostiantyn",
                Surname = "Salabai",
                Email = "Salabai@gmail.com",
                Password = UnitTestHelper.Encrypt("qwerty"),
                Role = Roles.Programmer
            };
            return new List<Programmer>()
            {
                new Programmer {
                     Id = 1,
                     ManagerId = 1,
                     User = user1,
                     Skills = new List<Skill>() { skill1 }
                },
                new Programmer {
                     Id = 2,
                     ManagerId = 1,
                     User = user2,
                     Skills = new List<Skill>() { skill2 }
                }
            };
        }

        //private static IEnumerable<SkillModel> Skills =>
        //   new[]
        //   {
        //       new SkillModel { 
        //           Id = 1,
        //           Name = "DotNet",
        //           Lvl = "Advanced",
        //           ProgrammerId = 1
        //       },

        //       new SkillModel
        //       {
        //        Id = 2,
        //        Name = "JavaScript",
        //        Lvl = "Low",
        //        ProgrammerId = 2
        //       }
        //   };

    }
}
