using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Services;
using KnowledgeAccountingSystem.DAL.Entities;
using KnowledgeAccountingSystem.DAL.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnowledgeAccountingSystem.Tests.BusinessTests
{
    public class StatisticServiceTests
    {
        [Test]
        public void StatisticService_GetAverageCountProgrammersByManager_Return2()
        {
            //Arrange
            double expected = 2;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.ManagerRepository.FindAll())
                .Returns(GetTestManagerEntities().AsQueryable);
            var statisticService = new StatisticService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //Act
            var actual = statisticService.GetAverageCountProgrammersByManager();

            // Assert
                mockUnitOfWork
                    .Verify(uow => uow.ManagerRepository.FindAll(),
                    Times.Once());
                Assert.AreEqual(expected, actual);              
        }

        [Test]
        public void StatisticService_GetTop1Managers_ReturnsManagerModelWithoutProgrammers()
        {
            //Arrange
            const int COUNT_MANAGERS = 1;
            var comparer = new ManagerModelWithoutProgrammersComparer();
            var expected = GetTestManagerModelWithoutProgrammersModel();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.ManagerRepository.FindAll())
                .Returns(GetTestManagerEntities().AsQueryable);
            var statisticService = new StatisticService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //Act
            var actual = statisticService.GetTopManagers(COUNT_MANAGERS).ToList();

            //// Assert
            mockUnitOfWork
                .Verify(uow => uow.ManagerRepository.FindAll(),
                Times.Once());

            for (int i = 0; i < actual.Count(); i++)
            {
                Assert.IsTrue(comparer.Equals(expected[i], actual[i]));
            }
        }

        private List<Manager> GetTestManagerEntities()
        {
            var manager = new User
            {
                Id = 1,
                Name = "Jon",
                Surname = "Snow",
                Email = "Jon@gmail.com",
                Password = UnitTestHelper.Encrypt("qwerty"),
                Role = Roles.Manager
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

            return new List<Manager>()
            {
               new Manager
               {
                  Id = 1,
                  User = manager,
                 Programmers = new List<Programmer>() { 
                       new Programmer
                       {
                          Id = 1,
                          ManagerId = 1,
                          User = user1
                       },
                        new Programmer
                        {
                          Id = 2,
                          ManagerId = 1,
                          User = user2
                        }
                  }
               }
            };
        }
        private List<ManagerModelWithoutProgrammers> GetTestManagerModelWithoutProgrammersModel()
        {
            return new List<ManagerModelWithoutProgrammers>()
            {
                new ManagerModelWithoutProgrammers
                {
                Id = 1,
                Name = "Jon",
                Surname = "Snow"
                }
            };         
        }
    }
}
