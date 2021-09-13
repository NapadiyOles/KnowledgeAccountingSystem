using System.Linq;
using System.Threading.Tasks;
using KnowledgeAccountingSystem.DAL;
using KnowledgeAccountingSystem.DAL.Entities;
using KnowledgeAccountingSystem.DAL.Repositories;
using NUnit.Framework;

namespace KnowledgeAccountingSystem.Tests.DataTests
{
    [TestFixture]
    public class ProgrammerRepositoryTests
    {
        [Test]
        public void ProgrammerRepository_FindAll_ReturnsAllValues()
        {
            using (var context = new KnowledgeDbContext(UnitTestHelper.GetUnitTestDbOptions()))
            {
                var programmerRepository = new ProgrammerRepository(context);

                var programmers = programmerRepository.FindAll();

                Assert.AreEqual(2, programmers.Count());
            }
        }

        [Test]
        public async Task ProgrammerRepository_GetById_ReturnsSingleValue()
        {
            using (var context = new KnowledgeDbContext(UnitTestHelper.GetUnitTestDbOptions()))
            {
                var programmerRepository = new ProgrammerRepository(context);

                var programmer = await programmerRepository.GetByIdAsync(1);

                Assert.AreEqual(1, programmer.Id);
                Assert.AreEqual("Maria", programmer.User.Name);
                Assert.AreEqual("Soroka", programmer.User.Surname);
            }
        }

        [Test]
        public async Task ProgrammerRepository_AddAsync_AddsValueToDatabase()
        {
            using (var context = new KnowledgeDbContext(UnitTestHelper.GetUnitTestDbOptions()))
            {
                var programmersRepository = new ProgrammerRepository(context);
                var programmer = new Programmer() { Id = 3 };

                await programmersRepository.AddAsync(programmer);
                await context.SaveChangesAsync();

                Assert.AreEqual(3, context.Programmers.Count());
            }
        }

        [Test]
        public async Task ProgrammerRepository_DeleteByIdAsync_DeletesEntity()
        {
            using (var context = new KnowledgeDbContext(UnitTestHelper.GetUnitTestDbOptions()))
            {
                var programmersRepository = new ProgrammerRepository(context);

                await programmersRepository.DeleteByIdAsync(1);
                await context.SaveChangesAsync();

                Assert.AreEqual(1, context.Programmers.Count());
            }
        }

        [Test]
        public async Task ProgrammerRepository_Update_UpdatesEntity()
        {
            using (var context = new KnowledgeDbContext(UnitTestHelper.GetUnitTestDbOptions()))
            {
                var programmersRepository = new ProgrammerRepository(context);

                var programmer = new Programmer() { 
                    Id = 2,
                    ManagerId = null
                };

                programmersRepository.Update(programmer);
                await context.SaveChangesAsync();

                Assert.AreEqual(2, programmer.Id);
                Assert.AreEqual(null, programmer.ManagerId);
            }
        }
    }
}
