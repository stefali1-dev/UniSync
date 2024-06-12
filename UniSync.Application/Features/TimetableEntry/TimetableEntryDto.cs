using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSync.Application.Features.TimetableEntry
{
    public class TimetableEntryDto
    {
        public string TimeInterval { get; set; }
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseType { get; set; }

        public string ProfessorId { get; set; }
        public string ProfessorName { get; set; }
        public string Classroom { get; set; }

        public int DayOfWeek { get; set; }
        public string StudentGroup { get; set; }
    }
}
