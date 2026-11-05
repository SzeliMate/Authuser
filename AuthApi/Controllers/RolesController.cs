using AuthApi.Models;
using AuthApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Controllers
{
    [Route("roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly AuthuserContext _context;
        public RolesController(AuthuserContext context)
        {
            _context = context;
        }
        [HttpPost("newRole")]
        public async Task<IActionResult> NewRole([FromBody] NewRoleDto newRoleDto)
        {
            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = newRoleDto.Name,
                RegTime = newRoleDto.RegTime,
            };

            if (role is null)
            {
                return StatusCode(400, new { message = "Hiba" });
            }

            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();

            return StatusCode(201, new { message = "Felvéve", result = role });
        }

        [HttpGet("allRoles")]
        public async Task<IActionResult> AllRoles()
        {
            var roles = await _context.Roles.ToListAsync();

            if (roles is null)
            {
                return StatusCode(400, new { message = "Hiba" });
            }

            return StatusCode(200, new { message = "Lekérdezve", result = roles });
        }

        [HttpGet("byroleId")]
        public async Task<IActionResult> RoleById([FromQuery] Guid id)
        {
            var role = await _context.Roles.SingleOrDefaultAsync(r => r.Id == id);

            if (role is null)
            {
                return StatusCode(404, new { message = "Nincs találat" });
            }
            else if (role is not null)
            {
                return StatusCode(200, new { message = "Lekérdezve", result = role });
            }

            return StatusCode(400, new { message = "Hiba" });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateRole([FromQuery] Guid id, [FromBody] UpdateRoleDto updateRoleDto)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);

            if (role is null)
            {
                return StatusCode(404, new { message = "Nincs találat" });
            }
            else if (role is not null)
            {
                role.Id = updateRoleDto.Id;
                role.Name = updateRoleDto.Name;
                

                _context.Roles.Update(role);
                await _context.SaveChangesAsync();

                return StatusCode(200, new { message = "Frissítve", result = role });
            }

            return StatusCode(400, new { message = "Hiba" });
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteRole([FromQuery] Guid id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);

            if (role is null)
            {
                return StatusCode(404, new { message = "Nincs találat" });
            }
            else if (role is not null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();

                return StatusCode(200, new { message = "Törölve", result = role });
            }

            return StatusCode(400, new { message = "Hiba" });
        }
    }
}
