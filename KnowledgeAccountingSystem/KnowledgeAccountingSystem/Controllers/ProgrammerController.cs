using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Interfaces;
using KnowledgeAccountingSystem.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KnowledgeAccountingSystem.BLL.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeAccountingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionFilter]
    [Authorize(Roles = DAL.Entities.Roles.Programmer)]
    public class ProgrammerController : ControllerBase
    {
        private readonly IProgrammerService service;
        private int programmerId => service.GetRoleId(userId);
        private int userId => int.Parse(User.Claims.ElementAt(0).Value);

        public ProgrammerController(IProgrammerService service)
        {
            this.service = service;
        }

        /// <summary>
        /// This method get all programer`s skills.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillModel>>> GetSkills()
        {
            return Ok(await service.GetSkillsAsync(programmerId));
        }

        /// <summary>
        /// This method get programmer`s skill by id.
        /// </summary>
        /// <param name="skillId"></param>
        /// <exception cref="InvalidModelException">incorrect skill id</exception>
        /// <returns></returns>
        [HttpGet("getSkill/{skillId}")]
        public async Task<ActionResult<SkillModel>> GetSkillById(int skillId)
        {
            return Ok(await service.GetProgrammerSkillByIdAsync(programmerId, skillId));
        }

        /// <summary>
        /// This method add skill to programmer.
        /// </summary>
        /// <param name="skill"></param>
        /// <exception cref="ResourceAlreadyExistException">exist skill</exception>
        /// <exception cref="InvalidModelException">incorrect skill model</exception>
        /// <returns></returns>
        [HttpPost("addSkill")]
        public ActionResult AddSkill(SkillModel skill)
        {
            service.AddSkill(programmerId, skill);
            return Ok();
        }

        /// <summary>
        /// This method update programmer`s skills.
        /// </summary>
        /// <param name="skill"></param>
        /// <exception cref="ResourceAlreadyExistException">skill is not found</exception>
        /// <exception cref="InvalidModelException">incorrect skill model</exception>
        /// <returns></returns>
        [HttpPut("updateSkill")]
        public async Task<ActionResult> UpdateSkill(SkillModel skill)
        {
            await service.EditSkillAsync(programmerId, skill);
            return Ok();
        }

        /// <summary>
        /// This method delete programmer`s skills.
        /// </summary>
        /// <param name="skillId"></param>
        /// <exception cref="ResourceAlreadyExistException">skill is not found</exception>
        /// <returns></returns>
        [HttpDelete("removeSkill/{skillId}")]
        public async Task<ActionResult> DeleteSkill(int skillId)
        {
            await service.DeleteSkillAsync(programmerId, skillId);
            return Ok();
        }

        /// <summary>
        /// This method update programmer account.
        /// </summary>
        /// <param name="model"></param>
        /// <exception cref="InvalidModelException">model incorrect</exception>
        /// <returns></returns>
        [HttpPut("account")]
        public async Task<ActionResult> UpdateProgrammerAccount([FromBody] UserModel model)
        {
            model.Id = userId;
            await service.UpdateAccountAsync(model);
            return Ok();
        }

        /// <summary>
        /// This method delete programmer account.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("account")]
        public async Task<ActionResult> DeleteProgrammerAccount()
        {
            await service.DeleteAccountAsync(programmerId);
            return Ok();
        }
    }
}
