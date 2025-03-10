using AutoMapper;
using MediatR;
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

        #region [- Ctor -]
        public TechnicalOfficerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region [- Get() -]

        [HttpGet("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ApiResult<GetTechnicalOfficerDto>> GetById([FromQuery] GetTechnicalOfficerByIdQuery query, CancellationToken cancellationToken = default)
        {
            var officer = await _mediator.Send(query, cancellationToken);
            return Ok(officer);
        }
        #endregion

        #region [- Post() -]

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ApiResult<Guid>> Create([FromBody] CreateTechnicalOfficerCommand command, CancellationToken cancellationToken = default)
        {
            
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        #endregion

        #region [- Delete() -]

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ApiResult> Delete(Guid Id, CancellationToken cancellationToken = default)
        {
            var command = new DeleteTechnicalOfficerCommand() { Id = Id, };
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }
        #endregion

    }
}
