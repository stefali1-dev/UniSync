using Microsoft.AspNetCore.Mvc;
using UniSync.API.Controllers;
using UniSync.Application.Features.Students.Queries.GetByGroup;
using UniSync.Application.Features.Users.Queries.GetById;

namespace UniSync.Api.Controllers;

[ApiController]
public class ChatsController : ApiControllerBase
{
    //[HttpGet("ByUserId/{id}")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //public async Task<IActionResult> GetChatsByUserId(string id)
    //{
    //    var query = new GetByIdUserQuery { UserId = id };
    //    var result = await Mediator.Send(query);
    //    if (!result.Success)
    //    {
    //        if (result.Message == $"User with id {id} not found")
    //        {
    //            return NotFound(result);
    //        }
    //        return BadRequest(result);
    //    }
    //    return Ok(result);
    //}
}