using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TODO.Application.Contracts.Service;
using TODO.Domain.DTO.User;

namespace TODO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        // GET api/user
        [HttpGet]
        public async Task<ActionResult> GetAllUser()
        {
            var responce = await userService.GetAllUser();
            return Ok(responce);
        }
        // POST api/user/insert
        [HttpPost]
        public async Task<ActionResult> RegistrationUser([FromBody] InsertUserDto dto)
        {
            if (dto is null) return BadRequest();

            var createUser = await userService.RegistrationUserAsync(dto);

            return Ok(createUser);
        }

        
    }
}
