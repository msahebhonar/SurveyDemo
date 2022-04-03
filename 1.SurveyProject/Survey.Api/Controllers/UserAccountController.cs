using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Survey.Common;
using Survey.Models.Login;
using Survey.Services;

namespace Survey.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class UserAccountController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(statusCode:StatusCodes.Status200OK, type:typeof(UserAccountDto))]
        [ProducesDefaultResponseType]
        public IActionResult Login([FromServices] IUserAccountRepository useAccountRepository, [FromBody] LoginDto model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
                    return BadRequest(ErrorMessages.InvalidUserInfo);

                var user = useAccountRepository.Login(model.Email, model.Password);
                if (user is null)
                    return Unauthorized();

                return Ok(UserAccountDto.ConvertToDto(user));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
