using UniSync.Application.Persistence;
using UniSync.Infrastructure.Repositories;
using UniSync.Infrastructure;
using UniSync.Domain.Entities;
using UniSync.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class StudentRepository : BaseRepository<Student>, IStudentRepository
{
    public StudentRepository(UniSyncContext context) : base(context)
    {

    }

    public async Task<Result<Student>> FindByChatUserId(Guid chatUserId)
    {
        var student = await context.Students.SingleOrDefaultAsync(s => s.ChatUserId == chatUserId);

        if (student == null)
        {
            return Result<Student>.Failure($"No student found with ChatUserId: {chatUserId}");
        }

        return Result<Student>.Success(student);
    }


    public async Task<Result<IReadOnlyList<Student>>> GetStudentsByGroupAsync(string groupName)
    {
        var students = await context.Students
            .Where(u => u is Student)
            .Cast<Student>()
            .Where(s => s.Group == groupName)
            .AsNoTracking()
            .ToListAsync();

        if (students.Count == 0)
        {
            return Result<IReadOnlyList<Student>>.Failure($"Students with group {groupName} not found");
        }

        return Result<IReadOnlyList<Student>>.Success(students);
    }

}