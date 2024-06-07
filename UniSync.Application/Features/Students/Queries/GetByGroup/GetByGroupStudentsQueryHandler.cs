using MediatR;
using UniSync.Application.Persistence;

namespace UniSync.Application.Features.Students.Queries.GetByGroup
{
    public class GetByGroupStudentsQueryHandler : IRequestHandler<GetByGroupStudentsQuery, GetByGroupStudentsQueryResponse>
    {
        private readonly IStudentRepository studentRepository;

        public GetByGroupStudentsQueryHandler(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        public async Task<GetByGroupStudentsQueryResponse> Handle(GetByGroupStudentsQuery request, CancellationToken cancellationToken)
        {
            var result = await studentRepository.GetStudentsByGroupAsync(request.GroupName);
            if (!result.IsSuccess)
                return new GetByGroupStudentsQueryResponse { Success = false, Message = result.Error };

            GetByGroupStudentsQueryResponse response = new();

            response.Students = result.Value.Select(u => new StudentDto
            {
                StudentId = u.StudentId,
                ChatUserId = u.ChatUserId,
                Semester = u.Semester,
                Group = u.Group,
                CoursesIds = u.Courses.Select(c => c.CourseId.ToString()).ToList(),

            }).ToList();

            return response;
        }
    }
}
