using Microsoft.AspNetCore.Mvc;
using UniSync.API.Controllers;
using UniSync.Application.Contracts.Interfaces;
using UniSync.Application.Features.Courses;
using UniSync.Application.Features.Students;
using UniSync.Application.Persistence;

namespace UniSync.Api.Controllers;

[ApiController]
public class CoursesController : ApiControllerBase
{
    private readonly ICoursesService coursesService;
    private readonly ICourseRepository courseRepository;

    public CoursesController(ICoursesService coursesService, ICourseRepository courseRepository)
    {
        this.coursesService = coursesService;
        this.courseRepository = courseRepository;
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


    [HttpGet("ById/{courseId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCourseById(string courseId)
    {
        var result = await this.courseRepository.FindByIdAsync(new Guid(courseId));
        var course = result.Value;
        var courseDto = new CourseDto
        {
            CourseId = course.CourseId.ToString(),
            CourseName = course.CourseName,
            CourseNumber = course.CourseNumber,
            Credits = course.Credits,
            Description = course.Description,
            Semester = course.Semester,
            ProfessorsIds = course.Professors.Select(p => p.ProfessorId.ToString()).ToList(),
            StudentsIds = course.Students.Select(s => s.StudentId.ToString()).ToList()
        };


        if (result == null)
        {
            return NotFound($"Student with ID {courseId} not found");
        }
        return Ok(courseDto);
    }

}