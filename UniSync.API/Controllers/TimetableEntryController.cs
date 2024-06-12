using Microsoft.AspNetCore.Mvc;
using UniSync.API.Controllers;
using UniSync.Application.Persistence;
using UniSync.Domain.Entities.Administration;
using System.Threading.Tasks;
using UniSync.Application.Features.Evaluation;
using UniSync.Application.Contracts.Interfaces;
using UniSync.Application.Features.TimetableEntry;

namespace UniSync.Api.Controllers
{
    [ApiController]
    public class TimetableEntryController : ApiControllerBase
    {
        private readonly ITimetableEntryService timetableEntryService;

        public TimetableEntryController(ITimetableEntryService timetableEntryService)
        {
            this.timetableEntryService = timetableEntryService;
        }

        [HttpGet("{professorId}")]
        public async Task<IActionResult> GetTimetableEntriesByProfessorId(string professorId)
        {
            try
            {
                var timetableEntries = await timetableEntryService.GetTimetableEntriesByProfessorId(professorId);
                return Ok(timetableEntries);
            }
            catch (Exception ex)
            {
                // Log the exception details here
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }
}
