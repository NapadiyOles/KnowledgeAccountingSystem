using System.Linq;
using System.Threading.Tasks;
using KnowledgeAccountingSystem.DAL;
using KnowledgeAccountingSystem.DAL.Entities;
using KnowledgeAccountingSystem.DAL.Repositories;
using NUnit.Framework;

namespace KnowledgeAccountingSystem.Tests.DataTests
{
    [TestFixture]
    public class ManagerRepositoryTests
    {
        [Test]
        public void ManagerRepository_FindAll_ReturnsAllValues()
        {
            using (var context = new KnowledgeDbContext(UnitTestHelper.GetUnitTestDbOptions()))
            {
                var managerRepository = new ManagerRepository(context);

                var managers = managerRepository.FindAll();

                Assert.AreEqual(1, managers.Count());
            }
        }

        [Test]
        public async Task ManagerRepository_GetById_ReturnsSingleValue()
        {
            using (var context = new KnowledgeDbContext(UnitTestHelper.GetUnitTestDbOptions()))
            {
                var managerRepository = new ManagerRepository(context);

                var manager = await managerRepository.GetByIdAsync(1);

                Assert.AreEqual(1, manager.Id);
                Assert.AreEqual("Jon", manager.User.Name);
                Assert.AreEqual(Roles.Manager, manager.User.Role);
            }
        }

        [Test]
        public async Task ManagerRepository_AddAsync_AddsValueToDatabase()
        {
            using (var context = new KnowledgeDbContext(UnitTestHelper.GetUnitTestDbOptions()))
            {
                var managersRepository = new ManagerRepository(context);
                var manager = new Manager() { Id = 2 };

                await managersRepository.AddAsync(manager);
                await context.SaveChangesAsync();

                Assert.AreEqual(2, context.Managers.Count());
            }
        }

        [Test]
        public async Task ManagerRepository_DeleteByIdAsync_DeletesEntity()
        {
            using (var context = new KnowledgeDbContext(UnitTestHelper.GetUnitTestDbOptions()))
            {
                var managersRepository = new ManagerRepository(context);

                await managersRepository.DeleteByIdAsync(1);
                await context.SaveChangesAsync();

                Assert.AreEqual(0, context.Managers.Count());
            }
        }

        [Test]
        public async Task ManagerRepository_Update_UpdatesEntity()
        {
            using (var context = new KnowledgeDbContext(UnitTestHelper.GetUnitTestDbOptions()))
            {
                var managersRepository = new ManagerRepository(context);

                var manager = new Manager()
                {
                    Id = 1,
                    User = new User
                    {
                        Name = "Oleg",
                        Surname = "Lolo"
                    }
                };

                managersRepository.Update(manager);
                await context.SaveChangesAsync();

                Assert.AreEqual( 1, manager.Id,
                                 "Oleg", manager.User.Name,
                                 "Lolo", manager.User.Surname
                               );
            }
        }
    }
}
