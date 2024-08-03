using Microsoft.AspNetCore.Mvc;
using TestTaskMegapolis.Contracts;
using TestTaskMegapolis.DTOs.User;

namespace TestTaskMegapolis.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserRepository repository) : Controller
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("User creation failed");
        }

        await repository.CreateUser(userDto);

        return Ok("Success");
    }
    
    [HttpGet("get-users-with-groups")]
    public async Task<IActionResult> GetUsersWithGroups()
    {
        var result = await repository.GetUsersWithGroups();

        return Ok(result);
    }
}