using Microsoft.AspNetCore.Mvc;
using UniSync.API.Controllers;
using UniSync.Application.Contracts.Interfaces;
using UniSync.Application.Features.Channels.Commands.CreateChannel;
using UniSync.Application.Features.Channels.Queries;
using UniSync.Application.Features.Courses;
using UniSync.Domain.Entities;

namespace UniSync.Api.Controllers;

[ApiController]
public class CoursesController : ApiControllerBase
{
    private readonly ICoursesService coursesService;

    public CoursesController(ICoursesService coursesService)
    {
        this.coursesService = coursesService;
    }

    [HttpPost("load-csv")]
    public async Task<IActionResult> LoadCoursesFromCsv()
    {
        try
        {
            string csvPath = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "courses.csv");

            await coursesService.LoadCoursesFromCsv(csvPath);
            return Ok("Courses loaded successfully.");
        }
        catch (Exception ex)
        {
            // Log the exception details here
            return BadRequest($"An error occurred while loading courses: {ex.Message}");
        }
    }

}