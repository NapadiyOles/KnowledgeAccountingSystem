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
    [Authorize(Roles = DAL.Entities.Roles.Manager)]
    [ExceptionFilter]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService service;
        private int userId => int.Parse(User.Claims.ElementAt(0).Value);
        private int managerId => service.GetRoleId(userId);

        public ManagerController(IManagerService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProgrammerModel>> GetAllProgrammers()
        {
            return Ok(service.GetAllProgrammers());
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProgrammerModel>> GetProgrammersWithoutManagers()
        {
            return Ok(service.GetProgrammersWithoutManagers());
        }

        [HttpGet("choosen")]
        public async Task<ActionResult<IEnumerable<ProgrammerModel>>> GetChoosenProgrammers()
        {
            return Ok(await service.GetChoosenProgrammersByManagerIdAsync(managerId));
        }

        [HttpGet("choosen/{programmerId}")]
        public async Task<ActionResult<ProgrammerModel>> GetChoosenProgrammer(int programmerId)
        {
            return Ok(await service.GetChoosenProgrammerByManagerIdAsync(managerId, programmerId));
        }

        [HttpPost("subscribeProgrammer/{programmerId}")]
        public async Task<ActionResult> SubscribeProgrammer(int programmerId)
        {
            await service.SubscribeProgrammerAsync(managerId, programmerId);
            return Ok();
        }

        [HttpDelete("{programmerId}")]
        public async Task<ActionResult> UnSubscribeProgrammer(int programmerId)
        {
            await service.UnsubscribeProgrammerAsync(managerId, programmerId);
            return Ok();
        }

        [HttpPut("account")]
        public async Task<ActionResult> UpdateManagerAccount([FromBody] UserModel model)
        {
            model.Id = userId;
            await service.UpdateAccountAsync(model);
            return Ok();
        }

        [HttpDelete("account")]
        public async Task<ActionResult> DeleteManagerAccount()
        {
            await service.DeleteAccountAsync(managerId);
            return Ok();
        }
    }
}
