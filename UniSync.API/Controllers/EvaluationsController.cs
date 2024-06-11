using Microsoft.AspNetCore.Mvc;
using UniSync.API.Controllers;
using UniSync.Application.Persistence;
using UniSync.Domain.Entities.Administration;
using System.Threading.Tasks;
using UniSync.Application.Features.Evaluation;
using UniSync.Application.Contracts.Interfaces;

namespace UniSync.Api.Controllers
{
    [ApiController]
    public class EvaluationsController : ApiControllerBase
    {
        private readonly IEvaluationService evaluationService;

        public EvaluationsController(IEvaluationService evaluationService)
        {
            this.evaluationService = evaluationService;
        }

        [HttpPost]
        public async Task<IActionResult> AddEvaluation([FromBody] EvaluationDto evaluationDto)
        {
            try
            {
                //await Console.Out.WriteLineAsync(evaluationDto.StudentId);
                await evaluationService.AddEvaluation(evaluationDto);

                return Ok();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.StackTrace);
                return StatusCode(500, "Internal server error..");
            }
        }

        [HttpGet("Student/{studentId}")]
        public async Task<IActionResult> GetEvaluationsByStudentId(string studentId)
        {
            try
            {
                var evaluations = await evaluationService.GetByStudentIdAsync(studentId);
                return Ok(evaluations);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.StackTrace);
                return StatusCode(500, "Internal server error..");
            }
        }
    }
}
