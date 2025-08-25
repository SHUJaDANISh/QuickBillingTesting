using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickBiilingTesting.Data.Repository;
using QuickBiilingTesting.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
// Optionally add authorization attribute, e.g., [Authorize(Roles = "Admin")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<User>>> GetUserList()
    {
        var users = await _userRepository.GetAllUsers();
        return Ok(users);
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        var user = await _userRepository.GetUserById(id);
        if (user == null)
            return NotFound($"User with Id={id} not found.");

        return Ok(user);
    }
    
    [HttpPut("user/{id:int}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
    {
        user.Id = id;
        var updated = await _userRepository.UpdateUser(user);
        if (updated == 0) return NotFound();
        return NoContent();
    }
    [Authorize(Roles = "User")]
    [HttpDelete("user/{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var deleted = await _userRepository.DeleteUser(id);
        if (deleted == 0) return NotFound();
        return NoContent();
    }


}
