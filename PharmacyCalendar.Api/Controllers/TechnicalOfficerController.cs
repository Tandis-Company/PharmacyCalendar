using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyCalendar.Application.Features.Command;
using PharmacyCalendar.Application.Features.Dtos;
using PharmacyCalendar.Application.Features.Query;
using System.Net;
using Utilities.Framework;

namespace PharmacyCalendar.Api.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    [ApiController]
    public class TechnicalOfficerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        #region [- Ctor -]
        public TechnicalOfficerController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        #endregion

        #region [- Get() -]

        [HttpGet("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById([FromQuery] GetTechnicalOfficerByIdQuery query, CancellationToken cancellationToken = default)
        {
            var officer = await _mediator.Send(query, cancellationToken);
            return Ok(new ApiResult<TechnicalOfficeroutputDto>
            {
                IsSuccess = true,
                Data = officer
            });
        }
        #endregion

        #region [- Post() -]

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateTechnicalOfficerCommand command, CancellationToken cancellationToken = default)
        {
            
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new ApiResult<Guid>
            {
                IsSuccess = true,
                Message = "کاربر با موفقیت ثبت شد",
                Data = result
            });
        }

        #endregion

        #region [- Delete() -]

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid Id, CancellationToken cancellationToken = default)
        {
            var command = new DeleteTechnicalOfficerCommand() { Id = Id, };
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new ApiResult<bool>
            {
                IsSuccess = true,
                Message = "کاربر با موفقیت حذف شد",
            });
        }
        #endregion

    }
}
