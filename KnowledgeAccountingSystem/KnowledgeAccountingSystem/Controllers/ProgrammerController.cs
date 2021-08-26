using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Interfaces;
using KnowledgeAccountingSystem.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillModel>>> GetSkills()
        {
            return Ok(await service.GetSkillsAsync(programmerId));
        }

        [HttpGet("{skillId}")]
        public async Task<ActionResult<SkillModel>> GetSkillById(int skillId)
        {
            return Ok(await service.GetProgrammerSkillByIdAsync(programmerId, skillId));
        }

        [HttpPost]
        public async Task<ActionResult> AddSkill(SkillModel skill)
        {
            await service.AddSkillAsync(programmerId, skill);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSkill(SkillModel skill)
        {
            await service.EditSkillAsync(programmerId, skill);
            return Ok();
        }

        [HttpDelete("{skillId}")]
        public async Task<ActionResult> DeleteSkill(int skillId)
        {
            await service.DeleteSkillAsync(programmerId, skillId);
            return Ok();
        }

        [HttpPut("account")]
        public async Task<ActionResult> UpdateProgrammerAccount([FromBody] UserModel model)
        {
            model.Id = userId;
            await service.UpdateAccountAsync(model);
            return Ok();
        }

        [HttpDelete("account")]
        public async Task<ActionResult> DeleteProgrammerAccount()
        {
            await service.DeleteAccountAsync(programmerId);
            return Ok();
        }
    }
}
