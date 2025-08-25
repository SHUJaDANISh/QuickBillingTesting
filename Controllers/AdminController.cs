using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickBiilingTesting.Data.Repository;
using QuickBiilingTesting.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public AdminController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("admins")]
    public async Task<ActionResult<IEnumerable<User>>> GetAdmins()
    {
        return Ok(await _userRepository.GetAdmins());
    }

    [HttpGet("admin/{id:int}")]
    public async Task<ActionResult<User>> GetAdminById(int id)
    {
        var admin = await _userRepository.GetAdminById(id);
        if (admin == null) return NotFound();
        return Ok(admin);
    }

    [HttpPost("admin")]
    public async Task<ActionResult<int>> CreateAdmin([FromBody] User admin)
    {
        admin.Role = "Admin";
        admin.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password);
        var id = await _userRepository.CreateAdmin(admin);
        return Ok(id);
    }

    [HttpPut("admin/{id:int}")]
    public async Task<IActionResult> UpdateAdmin(int id, [FromBody] User admin)
    {
        admin.Id = id;
        var updated = await _userRepository.UpdateAdmin(admin);
        if (updated == 0) return NotFound();
        return NoContent();
    }

    [HttpDelete("admin/{id:int}")]
    public async Task<IActionResult> DeleteAdmin(int id)
    {
        var deleted = await _userRepository.DeleteAdmin(id);
        if (deleted == 0) return NotFound();
        return NoContent();
    }
}
