using Microsoft.AspNetCore.Mvc;
using UniSync.API.Controllers;
using UniSync.Application.Features.Students.Queries.GetByGroup;

namespace UniSync.Api.Controllers;

[ApiController]
public class StudentsController : ApiControllerBase
{
    [HttpGet("ByGroup/{groupName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStudentsByGroup(string groupName)
    {
        var query = new GetByGroupStudentsQuery { GroupName = groupName };
        var result = await Mediator.Send(query);
        if (!result.Success)
        {
            if (result.Message == $"Students with group {groupName} not found")
            {
                return NotFound(result);
            }
            return BadRequest(result);
        }
        return Ok(result);
    }
}