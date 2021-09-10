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

        /// <summary>
        /// This method get all programmer`s.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProgrammerModel>> GetAllProgrammers()
        {
            return Ok(service.GetAllProgrammers());
        }

        /// <summary>
        /// This method get all programmer`s without skills.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllProgrammersWithoutSkills")]
        public ActionResult<IEnumerable<ProgrammerModelWithoutSkills>> GetAllProgrammersWithoutSkills()
        {
            return Ok(service.GetAllProgrammersWithoutSkills());
        }

        /// <summary>
        /// This method get programmers who do not have a manager.
        /// </summary>
        /// <returns></returns>
        [HttpGet("programmersWithoutManagers")]
        public ActionResult<IEnumerable<ProgrammerModel>> GetProgrammersWithoutManagers()
        {
            return Ok(service.GetProgrammersWithoutManagers());
        }

        /// <summary>
        /// This method get choosen programmers.
        /// </summary>
        /// <returns></returns>
        [HttpGet("choosen")]
        public async Task<ActionResult<IEnumerable<ProgrammerModel>>> GetChoosenProgrammers()
        {
            return Ok(await service.GetChoosenProgrammersByManagerIdAsync(managerId));
        }

        /// <summary>
        /// This method get choosen programmer.
        /// </summary>
        /// <param name="programmerId"></param>
        /// <exception cref="InvalidModelException">Programmer is not found</exception>
        /// <returns></returns>
        [HttpGet("choosen/{programmerId}")]
        public async Task<ActionResult<ProgrammerModel>> GetChoosenProgrammer(int programmerId)
        {
            return Ok(await service.GetChoosenProgrammerByManagerIdAsync(managerId, programmerId));
        }

        /// <summary>
        /// This method choose programmer by manager.
        /// </summary>
        /// <param name="programmerId"></param>
        /// <exception cref="InvalidModelException">Programmer is not found</exception>
        /// <returns></returns>
        [HttpPost("subscribeProgrammer/{programmerId}")]
        public async Task<ActionResult> SubscribeProgrammer(int programmerId)
        {
            await service.SubscribeProgrammerAsync(managerId, programmerId);
            return Ok();
        }

        /// <summary>
        /// This method delete choosen programmer.
        /// </summary>
        /// <param name="programmerId"></param>
        /// <exception cref="InvalidModelException">Programmer is not found</exception>
        /// <returns></returns>
        [HttpDelete("unsubscribeProgrammer/{programmerId}")]
        public async Task<ActionResult> UnSubscribeProgrammer(int programmerId)
        {
            await service.UnsubscribeProgrammerAsync(managerId, programmerId);
            return Ok();
        }

        /// <summary>
        /// This method update manager account.
        /// </summary>
        /// <param name="model"></param>
        /// <exception cref="InvalidModelException">model incorrect</exception>
        /// <returns></returns>
        [HttpPut("account")]
        public async Task<ActionResult> UpdateManagerAccount([FromBody] UserModel model)
        {
            model.Id = userId;
            await service.UpdateAccountAsync(model);
            return Ok();
        }

        /// <summary>
        /// This method delete manager account.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("account")]
        public async Task<ActionResult> DeleteManagerAccount()
        {
            await service.DeleteAccountAsync(managerId);
            return Ok();
        }
    }
}
