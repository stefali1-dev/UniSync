using UniSync.Application.Features.Categories.Commands.CreateCategory;
using UniSync.Application.Features.Categories.Queries.GetAll;
using UniSync.Application.Features.Categories.Queries.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;

namespace UniSync.API.Controllers
{
    public class ProfessorController : ApiControllerBase
    {
        [Authorize(Roles = "User")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result.Category);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            //var result = await Mediator.Send(new GetAllProfessorsQuery());
            //return Ok(result.Professor);
            return Ok();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdCategoryQuery(id));
            return Ok(result);
        }


    }
}
