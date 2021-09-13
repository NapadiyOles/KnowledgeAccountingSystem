using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Services;
using KnowledgeAccountingSystem.BLL.Validation;
using KnowledgeAccountingSystem.DAL.Entities;
using KnowledgeAccountingSystem.DAL.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeAccountingSystem.Tests.BusinessTests
{
    public class ProgrammerServiceTests
    {
        [Test]
        public void ProgrammerService_GetProgrammersSkillsWithId1_ReturnsSkillModels()
        {
            // Arrange
            const int PROGRAMMER_ID = 1;
            var comparer = new SkillModelEqualityComparer();
            var expected = GetTestSkillModels().ToList();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.SkillRepository.GetAllProgrammersSkillsById(PROGRAMMER_ID))
                .Returns(GetTestSkillEntities().AsQueryable);
            var programmerService = new ProgrammerService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            // Act
            var actual = programmerService.GetSkillsAsync(PROGRAMMER_ID).Result.ToList();

            // Assert
            mockUnitOfWork
                .Verify(uow => uow.SkillRepository.GetAllProgrammersSkillsById(PROGRAMMER_ID), 
                Times.Once());
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.IsTrue(comparer.Equals(expected[i], actual[i]));
            }
        }

        [Test]
        public async Task ProgrammerService_DeleteSkill_DeleteEntity()
        {
            //Arrange
            const int PROGRAMMER_ID = 1;
            const int SKILL_ID = 1;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.SkillRepository.DeleteByIdAsync(It.IsAny<int>()));
            mockUnitOfWork.Setup(x => x.ProgrammerRepository.GetByIdAsync(PROGRAMMER_ID).Result)
                .Returns(GetTestProgrammerEntitie());

            var programmerService = new ProgrammerService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //Act
            await programmerService.DeleteSkillAsync(PROGRAMMER_ID, SKILL_ID);

            //Assert
            mockUnitOfWork.Verify(x => x.SkillRepository.DeleteByIdAsync(SKILL_ID), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }

        [Test]
        public void ProgrammerService_DeleteSkillWithInvalidId_ThrowsResourceAlreadyExistException()
        {
            //Arrange
            const int PROGRAMMER_ID = 1;
            const int SKILL_ID = 4;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.SkillRepository.DeleteByIdAsync(It.IsAny<int>()));
            mockUnitOfWork.Setup(x => x.ProgrammerRepository.GetByIdAsync(PROGRAMMER_ID).Result)
                .Returns(GetTestProgrammerEntitie());

            var programmerService = new ProgrammerService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            //Assert
            Assert.ThrowsAsync<ResourceAlreadyExistException>(() => programmerService.DeleteSkillAsync(PROGRAMMER_ID, SKILL_ID));
        }

        [Test]
        public void ProgrammerService_AddSkillAsync_AddsSkill()
        {
            //Arrange
            const int PROGRAMMER_ID = 1;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.SkillRepository.Add(It.IsAny<Skill>()));
            mockUnitOfWork.Setup(m => m.ProgrammerRepository.GetByIdAsync(PROGRAMMER_ID).Result)
                .Returns(GetTestProgrammerEntitie());
            var programmerService = new ProgrammerService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var skill = new SkillModel 
            { 
                Id = 3,
                Name = "SQL",
                Lvl = "Advanced",
                ProgrammerId = 1
            };
            //Act
            programmerService.AddSkill(PROGRAMMER_ID, skill);

            //Assert
            mockUnitOfWork.Verify(x => x.SkillRepository.Add(It.Is<Skill>(b => b.Id == skill.Id && b.Name.ToString() == skill.Name && b.Lvl.ToString() == skill.Lvl)), Times.Once);
            mockUnitOfWork.Verify(x => x.Save(), Times.Once);
        }

        [Test]
        public void ProgrammerService_AddSkillAsync_ThrowsKnowledgeAccountExceptionProgrammerNotFound()
        {
            const int PROGRAMMER_ID = 7;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.SkillRepository.Add(It.IsAny<Skill>()));
            mockUnitOfWork.Setup(m => m.ProgrammerRepository.GetByIdAsync(PROGRAMMER_ID).Result)
                .Returns(GetTestNullProgrammerEntitie());
            var programmerService = new ProgrammerService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var skill = new SkillModel 
            {
                Id = 3,
                Name = "SQL",
                Lvl = "Advanced",
                ProgrammerId = 1
            };

            Assert.Throws<KnowledgeAccountException>(() => programmerService.AddSkill(7, skill));
        }

        private IEnumerable<SkillModel> GetTestSkillModels()
        {
            return new List<SkillModel>()
            {
               new SkillModel
               {
                  Id = 1,
                  Name = "DotNet",
                  Lvl = "Advanced",
                  ProgrammerId = 1
               }
            };
        }
        private List<Skill> GetTestSkillEntities()
        {
            return new List<Skill>()
            {
               new Skill
               {
                  Id = 1,
                  Name = skillArea.DotNet,
                  Lvl = lvl.Advanced,
                  ProgrammerId = 1
               }
            };  
        }
        private Programmer GetTestProgrammerEntitie()
        {
            var user1 = new User
            {
                Id = 2,
                Name = "Maria",
                Surname = "Soroka",
                Email = "Maria@gmail.com",
                Password = UnitTestHelper.Encrypt("qwerty"),
                Role = Roles.Programmer
            };
            var skill1 = new Skill
            {
                Id = 1,
                Name = skillArea.DotNet,
                Lvl = lvl.Advanced,
                ProgrammerId = 1
            };
            return new Programmer
            {
                Id = 1,
                User = user1,
                ManagerId = 1,
                Skills = new List<Skill>() { skill1 }
            };   
        }

        private Programmer GetTestNullProgrammerEntitie()
        {
            return null;
        }

    }
}


