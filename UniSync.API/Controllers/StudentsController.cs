using Microsoft.AspNetCore.Mvc;
using UniSync.API.Controllers;
using UniSync.Application.Contracts.Interfaces;
using UniSync.Application.Features.Students;
using UniSync.Application.Features.Students.Queries.GetByGroup;
using UniSync.Application.Persistence;

namespace UniSync.Api.Controllers;

[ApiController]
public class StudentsController : ApiControllerBase
{
    private readonly IStudentRepository studentRepository;

    public StudentsController(IStudentRepository studentRepository)
    {
        this.studentRepository = studentRepository;
    }

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

    [HttpGet("ById/{studentId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStudentById(string studentId)
    {
        var result = await this.studentRepository.FindByIdAsync(new Guid(studentId));
        var stuent = result.Value;
        var studentDto = new StudentDto
        {
            StudentId = stuent.StudentId,
            ChatUserId = stuent.ChatUserId,
            Semester = stuent.Semester,
            Group = stuent.Group,
            CoursesIds = stuent.Courses.Select(c => c.CourseId.ToString()).ToList(),
        };


        if (result == null)
        {
            return NotFound($"Student with ID {studentId} not found");
        }
        return Ok(studentDto);
    }

    [HttpGet("ByChatUserId/{chatUserId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStudentByChatUserId(string chatUserId)
    {
        var result = await this.studentRepository.FindByChatUserId(new Guid(chatUserId));
        var stuent = result.Value;
        var studentDto = new StudentDto
        {
            StudentId = stuent.StudentId,
            ChatUserId = stuent.ChatUserId,
            Semester = stuent.Semester,
            Group = stuent.Group,
            CoursesIds = stuent.Courses.Select(c => c.CourseId.ToString()).ToList(),
        };


        if (result == null)
        {
            return NotFound($"Student with ChatUserId {chatUserId} not found");
        }
        return Ok(studentDto);
    }
}