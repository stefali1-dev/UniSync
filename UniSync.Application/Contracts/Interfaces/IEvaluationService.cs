using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSync.Application.Features.Courses;
using UniSync.Application.Features.Evaluation;
using UniSync.Domain.Entities.Administration;

namespace UniSync.Application.Contracts.Interfaces
{
    public interface IEvaluationService
    {
        public Task AddEvaluation(EvaluationDto evaluationDto);
        public Task<List<EvaluationDto>> GetByStudentIdAsync(string studentId);

    }
}
