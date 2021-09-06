using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Interfaces;
using KnowledgeAccountingSystem.Filters;
using KnowledgeAccountingSystem.BLL.Validation;
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

        /// <summary>
        /// This method get the most popular managers by programmers count.
        /// </summary>
        /// <param name="count"></param>
        /// <exception cref="KnowledgeAccountException"/>
        /// <returns></returns>
        [HttpGet("TopManagers/{count}")]
        public ActionResult<IEnumerable<ManagerModel>> GetTopManagers(int count)
        {          
                return Ok(service.GetTopManagers(count));          
        }

        /// <summary>
        /// This method get the most popular programmers skills.
        /// </summary>
        /// <param name="count"></param>
        /// <exception cref="KnowledgeAccountException"/>
        /// <returns></returns>
        [HttpGet("TopSkills/{count}")]
        public ActionResult<IEnumerable<DAL.Entities.skillArea>> GetTheMostPopularSkills(int count)
        {
                return Ok(service.GetTheMostPopularSkills(count));
        }

        /// <summary>
        /// This method get the least common programmers skills.
        /// </summary>
        /// <param name="count"></param>
        /// <exception cref="KnowledgeAccountException"/>
        /// <returns></returns>
        [HttpGet("UncommonSkills/{count}")]
        public ActionResult<IEnumerable<DAL.Entities.skillArea>> GetTheLeastСommonSkills(int count)
        {
            return Ok(service.GetTheLeastСommonSkills(count));
        }

        /// <summary>
        /// This method get average count programmers by manager.
        /// </summary>
        /// <returns></returns>
        [HttpGet("AverageCountProgrammersByManager")]
        public ActionResult<double> GetAverageCountProgrammersByManager()
        {
            return Ok(service.GetAverageCountProgrammersByManager());
        }

        /// <summary>
        /// This method get skills in which programmers have gaps.
        /// </summary>
        /// <returns></returns>
        [HttpGet("TheLeastPumpedSkills")]
        public ActionResult<IEnumerable<DAL.Entities.skillArea>> GetTheLeastPumpedSkills ()
        {
            return Ok(service.GetTheLeastPumpedSkills());
        }

        /// <summary>
        /// This method get skills in which programmers have gaps (by manager).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("TheLeastPumpedSkillsByManagerId/{id}")]
        public ActionResult<IEnumerable<DAL.Entities.skillArea>> GetTheLeastPumpedSkillsByManagerId(int id)
        {
            return Ok(service.GetTheLeastPumpedSkillsByManagerId(id));
        }
    }
}
