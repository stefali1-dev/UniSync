using Microsoft.AspNetCore.Mvc;
using UniSync.API.Controllers;
using UniSync.Application.Contracts.Interfaces;

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

    [HttpGet("ByProfessorId/{professorId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCoursesByProfessorId(string professorId)
    {
        try
        {
            var courses = await coursesService.GetCoursesByProfessorId(professorId);
            return Ok(courses);
        }
        catch
        {
            return BadRequest("Error retriving courses ");
        }
    }

    [HttpGet("ByStudentId/{studentId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCoursesByStudentId(string studentId)
    {
        try
        {
            var courses = await coursesService.GetCoursesByStudentId(studentId);
            return Ok(courses);
        }
        catch
        {
            return BadRequest("Error retriving courses ");
        }
    }

}