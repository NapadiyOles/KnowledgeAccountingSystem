using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.DAL;
using KnowledgeAccountingSystem.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeAccountingSystem.Tests.IntegrationTests
{
    [TestFixture]
    public class ProgrammerIntegrationTests
    {
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;
        private SkillModelEqualityComparer _skillModelEqualityComparer;
        private readonly string requestUri = "api/programmer/";

        [SetUp]
        public void Init()
        {
            _skillModelEqualityComparer = new SkillModelEqualityComparer();
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
            _client.DefaultRequestHeaders.Authorization =
                  new AuthenticationHeaderValue("Bearer", UnitTestHelper.GetProgrammersWithId1Token());
        }

        [Test]
        public async Task ProgrammerController_GetSkills_ReturnListSkillModel()
        {
            //arrange
            var expected = GetSkillModels().ToList();
            var expectedLength = expected.Count;

            //act
            var httpResponse = await _client.GetAsync("api/programmer");

            //assert
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.EnsureSuccessStatusCode();

            var actual = JsonConvert.DeserializeObject<IEnumerable<SkillModel>>(stringResponse).ToList();
            Assert.AreEqual(expectedLength, actual.Count);
            for (int i = 0; i < expectedLength; i++)
            {
                Assert.IsTrue(_skillModelEqualityComparer.Equals(expected[i], actual[i]));
            }

        }

        [Test]
        public async Task ProgrammerController_UseEndpoindWithoutAuthorize_ThrowsExceptionUnauthorize()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
            var httpResponse = await _client.GetAsync("api/programmer");

            Assert.That(httpResponse.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task ProgrammerController_UseEndpoindWithInvalidToken_ThrowsExceptionForbidden()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UnitTestHelper.GetManagersWithId1Token());
            var httpResponse = await _client.GetAsync("api/programmer");

            Assert.That(httpResponse.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
        }

        [Test]
        public async Task ProgrammerController_GetSkillById_ReturnSkillModel()
        {
            // arrange 
            const string REQUEST_URN = "getSkill/1";
            var expected = GetSkillModels().First();

            // act
            var httpResponse = await _client.GetAsync(requestUri + REQUEST_URN);

            // assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<SkillModel>(stringResponse);
            Assert.IsTrue(_skillModelEqualityComparer.Equals(expected, actual));
        }

        [Test]
        public async Task ProgrammerController_GetSkillById_ReturnBadRequest()
        {
            // arrange 
            const string REQUEST_URN = "getSkill/2";

            // act
            var httpResponse = await _client.GetAsync(requestUri + REQUEST_URN);
            // assert         
            Assert.That(httpResponse.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task ProgrammerController_AddSkill_AddEntityToDataBase()
        {
            // arrange
            string requestUrn = "addSkill";
            var skill = new SkillModel
            {
                Id = 3,
                Name = "HTML",
                Lvl = "Advanced",
                ProgrammerId = 1
            };
            var content = new StringContent(JsonConvert.SerializeObject(skill), Encoding.UTF8, "application/json");

            // act
            var httpResponse = await _client.PostAsync(requestUri + requestUrn, content);

            // assert
            httpResponse.EnsureSuccessStatusCode();
            await CheckSkillInfoIntoDb(skill, 3);

        }

        [Test]
        public async Task ProgrammerController_AddSkill_ThrowsExceptionIfModelIsIncorrect()
        {
            string requestUrn = "addSkill";

            // Name is empty
            var skillModel = new SkillModel 
            { 
                Id = 3, 
                Name = "", 
                Lvl="Low", 
                ProgrammerId=1
            }; 
            await CheckExceptionWhileAddNewModel(skillModel, requestUrn);

            // LVL is empty
            skillModel.Name = "ORM";
            skillModel.Lvl = "";

            await CheckExceptionWhileAddNewModel(skillModel, requestUrn);

            // Add skill that the programmer already has
            skillModel.Name = "DotNet";
            skillModel.Lvl = "Advanced";

            await CheckExceptionWhileAddNewModel(skillModel, requestUrn);
        }

        [Test]
        public async Task ProgrammerController_UpdateSkill_UpdateSkillInDb()
        {
            // arrange
            string requestUrn = "updateSkill";
            var skill = new SkillModel
            {
                Id = 1,
                Name = "DotNet",
                Lvl = "Low",
                ProgrammerId = 1
            };
            var content = new StringContent(JsonConvert.SerializeObject(skill), Encoding.UTF8, "application/json");

            // act
            var httpResponse = await _client.PutAsync(requestUri + requestUrn, content);

            //assert
            httpResponse.EnsureSuccessStatusCode();
            await CheckSkillInfoIntoDb(skill, 2);
        }

        [Test]
        public async Task ProgrammerController_UpdateSkill_ThrowsExceptionIfModelIsIncorrect()
        {
            string requestUrn = "updateSkill";
            // Name is empty
            var skill = new SkillModel
            {
                Id = 1,
                Name = "",
                Lvl = "Low",
                ProgrammerId = 1
            };
            await CheckExceptionWhileUpdateModel(skill, requestUrn);

            // LVL is empty
            skill.Name = "DotNet";
            skill.Lvl = "";
            await CheckExceptionWhileUpdateModel(skill, requestUrn);
        }

        [Test]
        public async Task ProgrammerController_DeleteSkill_DeleteSkillFromDb()
        {
            // arrange
            string requestUrn = "removeSkill/1";

            // act
            var httpResponse = await _client.DeleteAsync(requestUri + requestUrn);

            // assert
            httpResponse.EnsureSuccessStatusCode();
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<KnowledgeDbContext>();
                Assert.AreEqual(1, context.Skills.Count());
            }

        }

        #region helpMethods

        private async Task CheckExceptionWhileAddNewModel(SkillModel skill, string requestUrn)
            {
               var content = new StringContent(JsonConvert.SerializeObject(skill), Encoding.UTF8, "application/json");
               var httpResponse = await _client.PostAsync(requestUri + requestUrn, content);

               Assert.That(httpResponse.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            }

        private async Task CheckSkillInfoIntoDb(SkillModel skill, int expectedLength)
        {
            using (var test = _factory.Services.CreateScope())
            {
                var context = test.ServiceProvider.GetService<KnowledgeDbContext>();
                Assert.AreEqual(expectedLength, context.Skills.Count());

                var dbSkill = await context.Skills.FindAsync(skill.Id);
                Assert.AreEqual(skill.Id, dbSkill.Id);
                Assert.AreEqual(skill.Name, dbSkill.Name.ToString());
                Assert.AreEqual(skill.Lvl, dbSkill.Lvl.ToString());
            }
        }

        private async Task CheckExceptionWhileUpdateModel(SkillModel skill, string requestUrn)
        {
            var content = new StringContent(JsonConvert.SerializeObject(skill), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync(requestUri + requestUrn, content);

            Assert.That(httpResponse.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }


        private IEnumerable<SkillModel> GetSkillModels()
        {
            return new List<SkillModel>()
            {
                new SkillModel { Id = 1,
                  Name = "DotNet",
                  Lvl = "Advanced",
                  ProgrammerId = 1
                }
            };
        }

        #endregion

        [TearDown]
        public void TearDown()
        {
            _factory.Dispose();
            _client.Dispose();
        }

    }
}
