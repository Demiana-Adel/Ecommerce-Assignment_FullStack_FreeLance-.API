using Ecommerce_Assignment_FullStack_FreeLance_.Application.Services;
using Ecommerce_Assignment_FullStack_FreeLance_.Dtos.UserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Assignment_FullStack_FreeLance_.API.Controllers
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
        [HttpPost("UserRegister")]
        public async Task<IActionResult> RegisterAsync(RegisterDto register)
        {
            var data = await _userService.RegisterUser(register); 
            return Ok(data);
        }
        [HttpPost("UserLogin")]
        public async Task<IActionResult> LoginAsync(LoginDto login)
        {
            var data = await _userService.LoginUser(login);
            return Ok(data);
        }
        [HttpGet("GetALlUsers")]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _userService.GetAllPaginationUser(10, 1);
            return Ok(data);
        }
        [HttpGet("GetOneUser")]

        public async Task<IActionResult> GetOneAsync(Guid userId)
        {
            var data = await _userService.GetUserById(userId);
            return Ok(data);
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteAsync(Guid userId)
        {
            var data = await _userService.DeleteUser(userId);
            return Ok(data);

        }
    }
}
