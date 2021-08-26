using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Interfaces;
using KnowledgeAccountingSystem.BLL.Services;
using KnowledgeAccountingSystem.DAL.Entities;
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
    [Authorize(Roles = Roles.Administrator)]
    [ExceptionFilter]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServise service;

        public AdminController(IAdminServise service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserModel>> GetUsers()
        {
            return Ok(service.GetAllUsers());
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult> ChangeRole(int userId)
        {
            await service.ChangeRoleAsync(userId);
            return Ok();
        }

    }
}
