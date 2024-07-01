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
        private readonly ITimetableEntryRepository timetableEntryRepository;

        public TimetableEntryController(ITimetableEntryService timetableEntryService, ITimetableEntryRepository timetableEntryRepository)
        {
            this.timetableEntryService = timetableEntryService;
            this.timetableEntryRepository = timetableEntryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddTimetableEntry([FromBody] TimetableEntryDto timetableEntryDto)
        {
            try
            {
                await timetableEntryService.AddTimetableEntry(timetableEntryDto);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception details here
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpGet("ByProfessorId/{professorId}")]
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

        [HttpGet("ByStudentGroupName/{studentGroupName}")]
        public async Task<IActionResult> GetTimetableEntriesByStudentGroupName(string studentGroupName)
        {
            try
            {
                var timetableEntries = await timetableEntryService.GetTimetableEntriesByStudentGroupName(studentGroupName);
                return Ok(timetableEntries);
            }
            catch (Exception ex)
            {
                // Log the exception details here
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllTimetableEntries()
        {
            try
            {
                var timetableEntries = await timetableEntryRepository.GetAllAsync();
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

