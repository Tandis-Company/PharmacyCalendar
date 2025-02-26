using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PharmacyCalendar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnicalOfficerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public TechnicalOfficerController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
    }
}
