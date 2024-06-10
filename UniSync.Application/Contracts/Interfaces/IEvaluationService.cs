using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSync.Application.Features.Courses;
using UniSync.Application.Features.Evaluation;

namespace UniSync.Application.Contracts.Interfaces
{
    public interface IEvaluationService
    {
        public Task AddEvaluation(EvaluationDto evaluationDto);

    }
}
