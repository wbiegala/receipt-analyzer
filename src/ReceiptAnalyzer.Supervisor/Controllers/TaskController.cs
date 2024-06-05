using BS.ReceiptAnalyzer.Core.Commands.CreateAnalysisTask;
using BS.ReceiptAnalyzer.Supervisor.Contract;
using BS.ReceiptAnalyzer.Supervisor.Mappings;
using BS.ReceiptAnalyzer.Supervisor.Validation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BS.ReceiptAnalyzer.Supervisor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator
                ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("start")]
        public async Task<IActionResult> CreateTaskAsync([FromQuery] CreateAnalysisTask.Query? query, IFormFile file)
        {
            var validationResult = AnalysisTaskValidator.ValidateCreateAnalysisTaskRequest(file.ContentType, file.Length);

            if (!validationResult.IsValid)
                return UnprocessableEntity(validationResult.ToResponse());

            try
            {
                var command = new CreateAnalysisTaskCommand(
                    query?.Force ?? false,
                    file.OpenReadStream(),
                    file.ContentType);

                var result = await _mediator.Send(command);

                var response = CreateAnalysisTaskMapper.MapToResponse(result);

                return Ok(response);
            }
            catch (Exception ex) 
            {
                return BadRequest(ExceptionMapper.MapToResponse(ex));
            }
        }
    }
}
