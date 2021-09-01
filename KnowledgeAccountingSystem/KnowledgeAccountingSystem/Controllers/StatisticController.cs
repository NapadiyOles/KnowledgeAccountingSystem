using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Interfaces;
using KnowledgeAccountingSystem.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace KnowledgeAccountingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionFilter]
    public class StatisticController : ControllerBase
    {
        private readonly IStatService service;

        public StatisticController(IStatService service)
        {
            this.service = service;
        }

        [HttpGet("TopManagers/{count}")]
        public ActionResult<IEnumerable<ManagerModel>> GetTopManagers(int count)
        {          
                return Ok(service.GetTopManagers(count));          
        }

        [HttpGet("TopSkills/{count}")]
        public ActionResult<IEnumerable<DAL.Entities.skillArea>> GetTheMostPopularSkills(int count)
        {
                return Ok(service.GetTheMostPopularSkills(count));
        }

        [HttpGet("UncommonSkills/{count}")]
        public ActionResult<IEnumerable<DAL.Entities.skillArea>> GetTheLeastСommonSkills(int count)
        {
            return Ok(service.GetTheLeastСommonSkills(count));
        }

        [HttpGet("AverageCountProgrammersByManager")]
        public ActionResult<double> GetAverageCountProgrammersByManager()
        {
            return Ok(service.GetAverageCountProgrammersByManager());
        }

        [HttpGet("TheLeastPumpedSkills")]
        public ActionResult<IEnumerable<DAL.Entities.skillArea>> GetTheLeastPumpedSkills ()
        {
            return Ok(service.GetTheLeastPumpedSkills());
        }
    }
}
