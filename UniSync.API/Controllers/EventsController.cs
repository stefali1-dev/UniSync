using UniSync.Application.Features.Events.Commands.CreateEvent;
using UniSync.Application.Features.Events.Commands.DeleteEvent;
using UniSync.Application.Features.Events.Queries;
using UniSync.Application.Features.Events.Queries.GetEventDetail;
using UniSync.Application.Features.Events.Queries.GetEvents;
using Microsoft.AspNetCore.Mvc;

namespace UniSync.API.Controllers
{
    public class EventsController : ApiControllerBase
    {
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleteEventCommand = new DeleteEventCommand() { EventId = id };
            await Mediator.Send(deleteEventCommand);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CreateEventCommandResponse>> Create([FromBody] CreateEventCommand createEventCommand)
        {
            var response = await Mediator.Send(createEventCommand);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<EventDto>>> GetAll()
        {
            var dtos = await Mediator.Send(new GetEventsQuery());
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventDto>> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetEventDetailQuery()
            {
                EventId = id
            });
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result.Event);
        }
    }
}
