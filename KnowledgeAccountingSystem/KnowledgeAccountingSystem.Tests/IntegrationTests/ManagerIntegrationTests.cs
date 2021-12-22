using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.DAL;
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
    public class ManagerIntegrationTests
    {
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;
        private readonly string requestUri = "api/manager/";
        private ProgrammerModelEqualityComparer _programmerModelEqualityComparer;

        [SetUp]
        public void Init()
        {
            _programmerModelEqualityComparer = new ProgrammerModelEqualityComparer();
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UnitTestHelper.GetManagersWithId1Token());
        }

        [Test]
        public async Task ManagerController_GetAllProgrammers_ReturnListProgrammerModels()
        {
            var expected = GetProgrammerModels().ToList();
            var expectedLength = expected.Count;

            var httpResponse = await _client.GetAsync("api/manager");
            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            var actual = JsonConvert.DeserializeObject<IEnumerable<ProgrammerModel>>(stringResponse).ToList();

            actual.OrderBy(x => x.Id);
            Assert.AreEqual(expectedLength, actual.Count);
            
        }

        [Test]
        public async Task ManagerController_GetChoosenProgrammers_ReturnChoosenProgrammerModelsByManagerId()
        {
            string requestUrn = "choosen";
            var expected = GetProgrammerModels().ToList();
            var expectedLength = expected.Count;

            var httpResponse = await _client.GetAsync(requestUri + requestUrn);
            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            var actual = JsonConvert.DeserializeObject<IEnumerable<ProgrammerModel>>(stringResponse).ToList();

            Assert.AreEqual(expectedLength, actual.Count);
        }

        [Test]
        public async Task ManagerController_GetChoosenProgrammerWithId1_ReturnProgrammerModel()
        {
            // Arrange
            string requestUrn = "choosen/1";
            ProgrammerModel programmer = GetProgrammerModels().First();
            //Act
            var httpResponse = await _client.GetAsync(requestUri + requestUrn);
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();

            var actual = JsonConvert.DeserializeObject<ProgrammerModel>(stringResponse);

            //Assert
            Assert.That(actual, Is.EqualTo(programmer).Using(_programmerModelEqualityComparer), "Models are not equal");
        }

        [Test]
        public async Task ManagerController_GetChoosenProgrammerWithId3_ThrowBadRequestException()
        {
            //Arrange
            string requestUrn = "choosen/3";

            //Act
            var httpResponse = await _client.GetAsync(requestUri + requestUrn);

            //Assert
            Assert.That(httpResponse.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task ManagerController_UnSubscribeProgrammerWithId1_ReturnOk()
        {
            //Arrange
            string requestUrn = "unsubscribeProgrammer/2";

            //Act
            var httpResponse = await _client.DeleteAsync(requestUri + requestUrn);

            //Assert
            httpResponse.EnsureSuccessStatusCode();

            using (var test = _factory.Services.CreateScope())
            {
                const int MANAGER_ID = 1;
                var context = test.ServiceProvider.GetService<KnowledgeDbContext>();
                var programmers = context.Programmers.Where(x => x.ManagerId == MANAGER_ID);
                Assert.AreEqual(1, programmers.Count());
            }
        }

        [Test]
        public async Task ManagerController_UnsubscribeFromProgrammerToWhomManagerNotSubscribed_ThrowBadRequest()
        {
            //Arrange
            string requestUrn = "unsubscribeProgrammer/3";

            //Act
            var httpResponse = await _client.DeleteAsync(requestUri + requestUrn);

            //Assert
            Assert.That(httpResponse.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task ManagerController_DeleteManagerAccount_DeleteManagerFromDbAndReturnOk()
        {
            //Arrange
            
            //Act

            //Assert
        }

        [Test]
        public async Task ManagerController_UpdateManagerAccount_ThrowsExceptionIfModelIsIncorrect()
        {
            //Arrange

            //Act

            //Assert
        }

        [Test]
        public async Task ManagerController_UpdateManagerAccount_UpdateUserInDb()
        {
            //Arrange

            //Act

            //Assert
        }

        #region helpMethods

        private async Task CheckExceptionWhileUpdateModel(string reader)
        {
            var content = new StringContent(JsonConvert.SerializeObject(reader), Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync(requestUri, content);

            Assert.That(httpResponse.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }


        private IEnumerable<ProgrammerModel> GetProgrammerModels()
        {
            return new List<ProgrammerModel>()
            {
                new ProgrammerModel
                {
                    Id = 1,
                    Name = "Maria",
                    Surname = "Soroka"
                },
                new ProgrammerModel
                {
                    Id = 2,
                    Name = "Kostiantyn",
                    Surname = "Salabai"
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
