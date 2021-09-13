using System.Linq;
using System.Threading.Tasks;
using KnowledgeAccountingSystem.DAL;
using KnowledgeAccountingSystem.DAL.Entities;
using KnowledgeAccountingSystem.DAL.Repositories;
using NUnit.Framework;

namespace KnowledgeAccountingSystem.Tests.DataTests
{
    [TestFixture]
    public class SkillRepositoryTests
    {
        [Test]
        public void SkillRepository_FindAll_ReturnsAllValues()
        {
            using (var context = new KnowledgeDbContext(UnitTestHelper.GetUnitTestDbOptions()))
            {
                var skillRepository = new SkillRepository(context);

                var skills = skillRepository.FindAll();

                Assert.AreEqual(2, skills.Count());
            }
        }

        [Test]
        public async Task SkillRepository_GetById_ReturnsSingleValue()
        {
            using (var context = new KnowledgeDbContext(UnitTestHelper.GetUnitTestDbOptions()))
            {
                var skillRepository = new SkillRepository(context);

                var skill = await skillRepository.GetByIdAsync(1);

                Assert.AreEqual(1, skill.Id);
                Assert.AreEqual(1, skill.ProgrammerId);
                Assert.AreEqual(skillArea.DotNet, skill.Name);
                Assert.AreEqual(lvl.Advanced, skill.Lvl);
            }
        }

        [Test]
        public async Task SkillRepository_AddAsync_AddsValueToDatabase()
        {
            using (var context = new KnowledgeDbContext(UnitTestHelper.GetUnitTestDbOptions()))
            {
                var skillRepository = new SkillRepository(context);
                var skill = new Skill() 
                {
                    Id = 3,
                    Name = skillArea.CSS,
                    Lvl = lvl.Middle,
                    ProgrammerId = 1
                };

                await skillRepository.AddAsync(skill);
                await context.SaveChangesAsync();

                Assert.AreEqual(3, context.Skills.Count());
            }
        }

        [Test]
        public async Task SkillRepository_DeleteByIdAsync_DeletesEntity()
        {
            using (var context = new KnowledgeDbContext(UnitTestHelper.GetUnitTestDbOptions()))
            {
                var skillRepository = new SkillRepository(context);

                await skillRepository.DeleteByIdAsync(1);
                await context.SaveChangesAsync();

                Assert.AreEqual(1, context.Skills.Count());
            }
        }

        [Test]
        public async Task SkillRepository_Update_UpdatesEntity()
        {
            using (var context = new KnowledgeDbContext(UnitTestHelper.GetUnitTestDbOptions()))
            {
                var booksRepository = new SkillRepository(context);

                var skill = new Skill() 
                {
                    Id = 1,
                    Name = skillArea.Patterns,
                    Lvl = lvl.Advanced,
                    ProgrammerId = 1
                };

                booksRepository.Update(skill);
                await context.SaveChangesAsync();

                Assert.AreEqual(1, skill.Id);
                Assert.AreEqual(1, skill.ProgrammerId);
                Assert.AreEqual(skillArea.Patterns, skill.Name);
                Assert.AreEqual(lvl.Advanced, skill.Lvl);
            }
        }
    }
}
