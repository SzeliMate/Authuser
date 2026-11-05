using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuthApi.Models;

namespace AuthApi.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AuthuserContext _context;
        public UsersController(AuthuserContext context)
        {
            _context = context;
        }
        [HttpPost("newUser")]
        public async Task<IActionResult> NewUser([FromBody] NewUserDto newUserDto)
        {
            var user = new User
            { 
                Id = Guid.NewGuid(),
                Name = newUserDto.Name,
                Email = newUserDto.Email,
                Password = newUserDto.Password,
                RegTime = DateTime.Now
            };
            {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(NewUser), new { id = user.Id }, user);
        }
    }
}
