using WebApi.User;
using Microsoft.AspNetCore.Mvc;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Policy = "OnlyAAAAIsAuthorized")]
        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _userService.GetAllUsers();
        }

        [HttpPost]
        /*[Authorize(Policy = "OnlyAAAAIsAuthorized")]*/
        public async Task<IActionResult> CreateUser([FromBody] UserModel user)
        {
            if (user == null)
            {
                return BadRequest("User model is null.");
            }

            await _userService.CreateUser(user);
            return CreatedAtAction(nameof(CreateUser), user);
        }

    }
}