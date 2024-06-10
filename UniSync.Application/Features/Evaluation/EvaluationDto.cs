using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSync.Application.Features.Evaluation
{
    public class EvaluationDto
    {
        public string StudentId { get; set; }
        public string CourseId { get; set; }
        public string ProfessorId { get; set; }
        public int Grade { get; set; }
        public DateTime DateTime { get; set; }
        public string? Comment { get; set; }
    }
}
