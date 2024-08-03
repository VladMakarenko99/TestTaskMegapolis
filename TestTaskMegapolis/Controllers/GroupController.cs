using Microsoft.AspNetCore.Mvc;
using TestTaskMegapolis.Contracts;
using TestTaskMegapolis.DTOs.Group;

namespace TestTaskMegapolis.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupController(IGroupRepository repository) : Controller
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateGroup([FromBody] CreateGroupDto groupDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Group creation failed");
        }

        await repository.CreateGroup(groupDto);

        return Ok("Success");
    }
}