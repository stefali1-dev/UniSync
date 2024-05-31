using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSync.Domain.Entities;

namespace UniSync.Application.Features.Courses
{
    public class CourseDto
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseNumber { get; set; }
        public int Credits { get; set; }
        //public CourseType? Type { get; set; }
        public string Description { get; set; }
        public int Semester { get; set; }
        public List<string> ProfessorsIds { get; set; }
        public List<string> StudentsIds { get; set; }
    }
}
