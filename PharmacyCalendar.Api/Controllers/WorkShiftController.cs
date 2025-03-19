using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PharmacyCalendar.Application.Features.Command;
using PharmacyCalendar.Application.Features.Dtos.WorkShiftDto;

namespace PharmacyCalendar.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WorkShiftController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateWorkShiftCommand> _validator;

        #region [- Ctor -]
        public WorkShiftController(IMediator mediator, IValidator<CreateWorkShiftCommand> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }
        #endregion

        #region [- Post() -]

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] List<CreateWorkShiftCommand> commands, CancellationToken cancellationToken = default)
        {
            if (commands == null || !commands.Any())
                return BadRequest("At least one work shift must be provided.");

            var validationErrors = new List<string>();
            var results = new List<CreateWorkShiftDto>();

            foreach (var command in commands)
            {
                var validationResult = await _validator.ValidateAsync(command, cancellationToken);
                if (!validationResult.IsValid)
                {
                    validationErrors.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
                    continue;
                }

                var result = await _mediator.Send(command, cancellationToken);
                results.AddRange(result);
            }

            if (validationErrors.Any())
                return BadRequest(new { Errors = validationErrors });

            return Ok(results);
        }
        #endregion

    }
}
