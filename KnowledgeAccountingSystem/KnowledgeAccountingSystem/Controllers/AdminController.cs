using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Interfaces;
using KnowledgeAccountingSystem.BLL.Services;
using KnowledgeAccountingSystem.DAL.Entities;
using KnowledgeAccountingSystem.Filters;
using Microsoft.AspNetCore.Authorization;
using KnowledgeAccountingSystem.BLL.Validation;
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

        /// <summary>
        /// This method get all users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<UserModel>> GetUsers()
        {
            return Ok(service.GetAllUsers());
        }

        /// <summary>
        /// This method change user role.
        /// </summary>
        /// <param name="userId"></param>
        /// <exception cref="AuthorizeException">incorrect id</exception>
        /// <exception cref="ResourceAlreadyExistException">admin must by 1</exception>
        /// <exception cref="InvalidModelException">incorrect choosen id</exception>
        /// <returns></returns>
        [HttpPut("changeUserRole/{userId}")]
        public async Task<ActionResult> ChangeRole(int userId)
        {
            await service.ChangeRoleAsync(userId);
            return Ok();
        }

    }
}
