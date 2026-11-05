using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using sobrou.Application.Services.Interfaces;
using sobrou.Domain.DTOS;

namespace sobrou.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet("users")]
        [Authorize]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetUsersAsync());
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser([FromBody] LoginRequestDTO userData)
        {
            var result = await _userService.LoginUserAsync(userData);

            if(result.Value != null)
            {
                Response.Cookies.Append("access_token", result.Value.Token, 
                new CookieOptions 
                { 
                    HttpOnly = true, 
                    SameSite = SameSiteMode.Lax, 
                    Secure = false, 
                    Expires = DateTime.UtcNow.AddHours(2) 
                });
            }

            if (result.IsFailure)
            {
               return BadRequest(result?.Error?.Message); 
            }

            return Ok();
        } 

        [HttpPost("google")]
        public async Task<ActionResult> LoginUserWithGoogle([FromForm] IFormCollection googleData)
        {
            var res = googleData["credential"];

            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterRequestDTO userData)
        {
            var result = await _userService.RegisterUserAsync(userData);     

            if(result.Value != null)
            {
                Response.Cookies.Append("access_token", result.Value.Token, 
                new CookieOptions 
                { 
                    HttpOnly = true, 
                    SameSite = SameSiteMode.Lax, 
                    Secure = false, 
                    Expires = DateTime.UtcNow.AddHours(2) 
                });
            }
                        
            return Ok();
        }
    }
}
