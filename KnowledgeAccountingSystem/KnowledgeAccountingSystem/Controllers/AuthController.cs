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
    [AllowAnonymous]
    [Authorize]
    [Route("api/[controller]")]
    [ExceptionFilter]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService service;

        public AuthController(IAuthService service)
        {
            this.service = service;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginModel model)
        {
            var userToken = await service.LoginAsync(model.Email, model.Password);
            return Ok(userToken);
        }

        [HttpPost("registration")]
        public async Task<ActionResult> Registraton([FromBody] RegisterModel model)
        {
            await service.RegistrationAsync(new UserModel
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Password = model.Password
            });
            return Ok();
        }
    }
}
