using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Interfaces;
using KnowledgeAccountingSystem.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KnowledgeAccountingSystem.BLL.Validation;
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

        /// <summary>
        /// This method authorize users.
        /// </summary>
        /// <param name="model">login model</param>
        /// <exception cref="AuthorizeException">Unauthorized user</exception>
        /// <returns>token</returns>
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginModel model)
        {
            var userToken = await service.LoginAsync(model.Email, model.Password);
            return Ok(userToken);
        }

        /// <summary>
        /// This method add new user as programmer.
        /// </summary>
        /// <param name="model">register model</param>
        /// <exception cref="ResourceAlreadyExistException">exist email</exception>
        /// <exception cref="InvalidModelException">uncorrect register model</exception>
        /// <returns></returns>
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
