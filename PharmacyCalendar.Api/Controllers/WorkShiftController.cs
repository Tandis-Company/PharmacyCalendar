//using FluentValidation;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using PharmacyCalendar.Application.Features.Command;
//using PharmacyCalendar.Application.Features.Dtos.WorkShiftDto;
//using Utilities.Framework;

//namespace PharmacyCalendar.Api.Controllers
//{

//    [Route("[controller]")]
//    [ApiController]
//    public class WorkShiftController : ControllerBase
//    {
//        private readonly IValidator<CreateWorkShiftCommand> _individualValidator;
//        private readonly IValidator<List<CreateWorkShiftCommand>> _collectionValidator;
//        private readonly IMediator _mediator;

//        public WorkShiftController(
//            IValidator<CreateWorkShiftCommand> individualValidator,
//            IValidator<List<CreateWorkShiftCommand>> collectionValidator,
//            IMediator mediator)
//        {
//            _individualValidator = individualValidator;
//            _collectionValidator = collectionValidator;
//            _mediator = mediator;
//        }

//        public async Task<ApiResult<CreateWorkShiftDto>> Create([FromBody] CreateWorkShiftCommand command, CancellationToken cancellationToken = default)
//        {
//            var validationResult = _individualValidator.Validate(command);
//            if (!validationResult.IsValid)
//            {
//                return BadRequest(validationResult.Errors);
//            }
//            var validationWorkShiftResult = _collectionValidator.Validate((IValidationContext)command);
//            if (!validationResult.IsValid)
//            {
//                return BadRequest(validationResult.Errors);
//            }
//            var result = await _mediator.Send(command, cancellationToken);
//            return Ok(result);
//        }
//    }
//}
