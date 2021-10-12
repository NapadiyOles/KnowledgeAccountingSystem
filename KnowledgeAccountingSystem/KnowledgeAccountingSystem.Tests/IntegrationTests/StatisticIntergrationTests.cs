using KnowledgeAccountingSystem.BLL.DTO;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeAccountingSystem.Tests.IntegrationTests
{
    [TestFixture]
    public class StatisticIntergrationTests
    {
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;
        private ManagerModelWithoutProgrammersComparer _managersWithoutProgrammersComparer;
        private readonly string requestUri = "api/statistic/";

        [OneTimeSetUp]
        public void Init()
        {
            _managersWithoutProgrammersComparer = new ManagerModelWithoutProgrammersComparer();
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task StatisticController_GetAverageProgrammersCountByManager()
        {
            const string REQUEST_URN = "AverageCountProgrammersByManager";

            var httpResponse =  await _client.GetAsync(requestUri + REQUEST_URN);
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();

            var actual = JsonConvert.DeserializeObject<double>(stringResponse);

            Assert.That(actual, Is.EqualTo(ExpectedAverageProgrammersCountByManager));
        }

        [Test]
        public async Task StatisticController_GetTheLeastPumpedSkills_ReturnListWithSkillsName()
        {
            const string REQUEST_URN = "TheLeastPumpedSkills";

            var httpResponse = await _client.GetAsync(requestUri + REQUEST_URN);
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();

            var actual = JsonConvert.DeserializeObject<IEnumerable<string>>(stringResponse).ToList();

            Assert.That(actual.OrderBy(x => x), 
                Is.EqualTo(ExpectedTheLeastPumpedSkills));
        }

        [Test]
        public async Task StatisticController_GetTopManagers_ReturnManagerModelWithoutProgrammers()
        {
            const string REQUEST_URN = "TopManagers/1";

            var httpResponse = await _client.GetAsync(requestUri + REQUEST_URN);
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();

            var actual = JsonConvert.DeserializeObject<IEnumerable<ManagerModelWithoutProgrammers>>(stringResponse).ToList();

            Assert.That(actual.OrderBy(x => x.Id),
                Is.EqualTo(ExpectedTopManagers).Using(_managersWithoutProgrammersComparer));
        }

        [Test]
        public async Task StatisticController_GetTheMostPopularSkills_ReturnListSkillArea()
        {
            const string REQUEST_URN = "TopSkills/2";

            var httpResponse = await _client.GetAsync(requestUri + REQUEST_URN);
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();

            var actual = JsonConvert.DeserializeObject<IEnumerable<string>>(stringResponse).ToList();

            Assert.That(actual.OrderBy(x => x),
                Is.EqualTo(ExpectedTheMostPopularSkills));
        }

        [Test]
        public async Task StatisticController_GetTheLeastСommonSkills_ReturnListSkillArea()
        {
            const string REQUEST_URN = "UncommonSkills/3";

            var httpResponse = await _client.GetAsync(requestUri + REQUEST_URN);
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();

            var actual = JsonConvert.DeserializeObject<IEnumerable<string>>(stringResponse).ToList();

            Assert.That(actual.OrderBy(x => x),
                Is.EqualTo(ExpectedTheMostPopularSkills));
        }

        [Test]
        public async Task StatisticController_GetTheLeastPumpedSkillsByManagerId_ReturnListSkillNames()
        {
            const string REQUEST_URN = "TheLeastPumpedSkillsByManagerId/1";

            var httpResponse = await _client.GetAsync(requestUri + REQUEST_URN);
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();

            var actual = JsonConvert.DeserializeObject<IEnumerable<string>>(stringResponse).ToList();

            Assert.That(actual.OrderBy(x => x),
                Is.EqualTo(ExpectedTheLeastPumpedSkills));
        }

        private readonly double ExpectedAverageProgrammersCountByManager = 2;

        private IEnumerable<string> ExpectedTheLeastPumpedSkills =>
            new[]
            {
                new string("JavaScript")
            };

        private IEnumerable<ManagerModelWithoutProgrammers> ExpectedTopManagers =>
            new[]
            {
                new ManagerModelWithoutProgrammers()
                {
                    Id = 1,
                    Name = "Jon",
                    Surname = "Snow"
                }
            };

        private IEnumerable<string> ExpectedTheMostPopularSkills =>
            new[]
            {
                new string("DotNet"),
                new string("JavaScript")
            };

        [OneTimeTearDown]
        public void TearDown()
        {
            _factory.Dispose();
            _client.Dispose();
        }
    }
}
